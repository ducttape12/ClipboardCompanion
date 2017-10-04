using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClipboardCompanion.Services
{
    public class HotKeyService : IDisposable
    {
        private readonly HwndSource _source;
        private readonly IWindowsHotKeyApiService _windowsHotKeyApiService;
        private readonly IDictionary<int, HotKeyBinding> hotKeyBindingsById = new Dictionary<int, HotKeyBinding>();

        private int _nextUpHotKeyId;
        private int NextUpHotKeyId => _nextUpHotKeyId++;

        public HotKeyService(HwndSource source, IWindowsHotKeyApiService windowsHotKeyApiService)
        {
            _source = source;
            _windowsHotKeyApiService = windowsHotKeyApiService;
            _source?.AddHook(WndProc);
        }

        private const int HotKeyMessageTypeId = 0x0312;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var messageTypeId = msg;
            var hotKeyId = wParam;
            if (messageTypeId == HotKeyMessageTypeId && hotKeyBindingsById.ContainsKey((int)hotKeyId))
            {
                hotKeyBindingsById[(int)hotKeyId].OnHotKeyPressed();
            }
            
            return IntPtr.Zero;
        }

        public void Dispose()
        {
            _source?.Dispose();

            foreach (var id in hotKeyBindingsById.Keys)
            {
                UnregisterHotKey(id);
            }
        }

        public HotKeyBinding RegisterHotKey(IList<ModifierKeys> modifierKeys, Key key)
        {
            if (modifierKeys.Distinct().Count() != modifierKeys.Count)
            {
                throw new ArgumentException($"{nameof(modifierKeys)} must contain unique entries");
            }
            if (modifierKeys.Contains(ModifierKeys.Windows))
            {
                throw new ArgumentException("Windows modifier key is not supported for hot keys");
            }

            var id = NextUpHotKeyId;
            var binding = new HotKeyBinding
            {
                Id = id,
                OnHotKeyPressed = () => { }
            };
            hotKeyBindingsById[id] = binding;

            var modifierKeyFlags = (uint)modifierKeys.Sum(modifier => (int)modifier);
            if (!_windowsHotKeyApiService.RegisterHotKey(_source.Handle, id, modifierKeyFlags, (uint)KeyInterop.VirtualKeyFromKey(key)))
            {
                throw new ApplicationException($"Unable to register hot key combination of {string.Join("+", modifierKeys.Select(modifier => modifier.ToString()))}+{key}.");
            }

            return binding;
        }

        public void UnregisterHotKey(int id)
        {
            if (!hotKeyBindingsById.ContainsKey(id))
                throw new InvalidOperationException();

            if (_windowsHotKeyApiService.UnregisterHotKey(_source.Handle, id))
            {
                hotKeyBindingsById.Remove(id);
            }
            else
            {
                throw new ApplicationException("Unable to unregister previous hot key. Sorry!");
            }
        }
    }
}
