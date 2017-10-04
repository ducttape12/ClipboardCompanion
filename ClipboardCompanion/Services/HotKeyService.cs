using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClipboardCompanion.Services
{
    public class HotKeyService : IDisposable
    {
        private readonly HwndSource _source;
        private readonly IDictionary<int, HotKeyBinding> _hotKeyIdActionMapping = new Dictionary<int, HotKeyBinding>();

        private int _nextUpHotKeyId;
        private int NextUpHotKeyId => _nextUpHotKeyId++;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        public HotKeyService(HwndSource source)
        {
            _source = source;
            _source?.AddHook(WndProc);
        }

        private const int HotKeyMessageTypeId = 0x0312;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            var messageTypeId = msg;
            var hotKeyId = wParam;
            if (messageTypeId == HotKeyMessageTypeId && _hotKeyIdActionMapping.ContainsKey((int)hotKeyId))
            {
                _hotKeyIdActionMapping[(int)hotKeyId].OnHotKeyPressed();
            }
            
            return IntPtr.Zero;
        }

        public void Dispose()
        {
            _source?.Dispose();

            foreach (var id in _hotKeyIdActionMapping.Keys)
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
            _hotKeyIdActionMapping[id] = binding;

            var modifierKeyFlags = (uint)modifierKeys.Sum(modifier => (int)modifier);
            if (!RegisterHotKey(_source.Handle, id, modifierKeyFlags, (uint)KeyInterop.VirtualKeyFromKey(key)))
            {
                throw new ApplicationException($"Unable to register hot key combination of {string.Join("+", modifierKeys.Select(modifier => modifier.ToString()))}+{key}.");
            }

            return binding;
        }

        public void UnregisterHotKey(int id)
        {
            if (!_hotKeyIdActionMapping.ContainsKey(id))
                throw new InvalidOperationException();

            if (UnregisterHotKey(_source.Handle, id))
            {
                _hotKeyIdActionMapping.Remove(id);
            }
            else
            {
                throw new ApplicationException("Unable to unregister previous hot key. Sorry!");
            }
        }
    }
}
