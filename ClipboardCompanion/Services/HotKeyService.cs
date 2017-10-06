using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClipboardCompanion.Services
{
    public class HotKeyService : IHotKeyService
    {
        private readonly IWindowsHotKeyApiService _windowsHotKeyApiService;
        private readonly IDictionary<int, HotKeyBinding> _hotKeyBindingsById = new Dictionary<int, HotKeyBinding>();

        private int _nextUpHotKeyId;
        private readonly IntPtr _handle;
        private int NextUpHotKeyId => _nextUpHotKeyId++;

        public HotKeyService(HwndSource source, IWindowsHotKeyApiService windowsHotKeyApiService)
        {
            _handle = source.Handle;
            _windowsHotKeyApiService = windowsHotKeyApiService;

            source.AddHook(OnWindowMessageReceived);
        }

        private const int HotKeyMessageTypeId = 0x0312;

        private IntPtr OnWindowMessageReceived(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var messageTypeId = msg;
            var hotKeyId = wParam;
            if (messageTypeId == HotKeyMessageTypeId && _hotKeyBindingsById.ContainsKey((int)hotKeyId))
            {
                _hotKeyBindingsById[(int)hotKeyId].OnHotKeyPressed();
            }
            
            return IntPtr.Zero;
        }

        public HotKeyBinding RegisterHotKey(IList<ModifierKeys> modifierKeys, Key key)
        {
            if (modifierKeys.Distinct().Count() != modifierKeys.Count)
                throw new ArgumentException($"{nameof(modifierKeys)} must contain unique entries");
            if (modifierKeys.Contains(ModifierKeys.Windows))
                throw new ArgumentException("Windows modifier key is not supported for hot keys");

            var id = NextUpHotKeyId;
            var binding = new HotKeyBinding
            {
                Id = id,
                OnHotKeyPressed = () => { }
            };
            _hotKeyBindingsById[id] = binding;

            var modifierKeyFlags = (uint)modifierKeys.Sum(modifier => (int)modifier);
            if (!_windowsHotKeyApiService.RegisterHotKey(_handle, id, modifierKeyFlags, (uint)KeyInterop.VirtualKeyFromKey(key)))
            {
                throw new ApplicationException($"Unable to register hot key combination of {string.Join("+", modifierKeys.Select(modifier => modifier.ToString()))}+{key}.");
            }

            return binding;
        }

        public void UnregisterHotKey(HotKeyBinding hotKeyBinding)
        {
            if (!_hotKeyBindingsById.ContainsKey(hotKeyBinding.Id))
                throw new InvalidOperationException();

            if (_windowsHotKeyApiService.UnregisterHotKey(_handle, hotKeyBinding.Id))
            {
                _hotKeyBindingsById.Remove(hotKeyBinding.Id);
            }
            else
            {
                throw new ApplicationException("Unable to unregister previous hot key.");
            }
        }
    }
}
