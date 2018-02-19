using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interop;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.Services
{
    public class HotKeyService : IHotKeyService
    {
        private readonly IWindowsHotKeyApiService _windowsHotKeyApiService;
        private readonly IDictionary<int, HotKeyBinding> _hotKeyBindingsById = new Dictionary<int, HotKeyBinding>();
        private HwndSource _hwndSource;

        private int _nextUpHotKeyId;

        private bool _hookAdded;
        private IntPtr Handle
        {
            get
            {
                if (!_hookAdded)
                {
                    _hwndSource?.AddHook(OnWindowMessageReceived);
                    _hookAdded = true;
                }

                return _hwndSource?.Handle ?? IntPtr.Zero;
            }
        }

        /// <summary>
        ///  TODO: Potential race condition?
        /// </summary>
        private int NextUpHotKeyId => _nextUpHotKeyId++;

        public bool IsInitialized { get; private set; }

        public HotKeyService(IWindowsHotKeyApiService windowsHotKeyApiService)
        {
            _windowsHotKeyApiService = windowsHotKeyApiService;
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

        public void RegisterWindowHandle(HwndSource source)
        {
            _hwndSource = source;
            IsInitialized = true;
            Initialized?.Invoke(this, new EventArgs());
        }

        public event EventHandler<EventArgs> Initialized;

        public HotKeyBinding RegisterHotKey(IList<ModifierKeys> modifierKeys, Key key)
        {
            if (!IsInitialized)
            {
                throw new ApplicationException(
                    "A window handle must be registered to the HotKeyService before hot keys can be registered.");
            }
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
            _hotKeyBindingsById[id] = binding;

            var modifierKeyFlags = (uint)modifierKeys.Sum(modifier => (int)modifier);
            if (!_windowsHotKeyApiService.RegisterHotKey(Handle, id, modifierKeyFlags, (uint)KeyInterop.VirtualKeyFromKey(key)))
            {
                throw new ApplicationException($"Unable to register hot key combination of {string.Join("+", modifierKeys.Select(modifier => modifier.ToString()))}+{key}.");
            }

            return binding;
        }

        public void UnregisterHotKey(HotKeyBinding hotKeyBinding)
        {
            if (!IsInitialized)
            {
                throw new ApplicationException(
                    "A window handle must be registered to the HotKeyService before hot keys can be registered.");
            }
            if (!_hotKeyBindingsById.ContainsKey(hotKeyBinding.Id))
            {
                throw new InvalidOperationException();
            }

            if (_windowsHotKeyApiService.UnregisterHotKey(Handle, hotKeyBinding.Id))
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
