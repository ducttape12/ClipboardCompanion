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
        private HwndSource _source;
        private IDictionary<int, Action> _hotKeyIdActionMapping = new Dictionary<int, Action>();

        private int _nextUpHotKeyId;
        private int NextUpHotKeyId => _nextUpHotKeyId++;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// Construct a new HotKeyService
        /// </summary>
        /// <param name="source">The source that will be associated with WinAPI calls</param>
        public HotKeyService(HwndSource source)
        {
            _source = source;
            _source?.AddHook(WndProc);
        }

        private const int WmHotkey = 0x0312;

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WmHotkey && _hotKeyIdActionMapping.ContainsKey((int)wParam))
            {
                _hotKeyIdActionMapping[(int) wParam]();
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

        /// <summary>
        /// Registers a new hot key, returning the ID associated with this hot key
        /// </summary>
        /// <param name="modifiers">Any modifiers associated with this hot key combination</param>
        /// <param name="key">The key associated with this hot key combination</param>
        /// <param name="callback">The action that will be performed if this hot key combination is performed</param>
        /// <param name="previousHotKeyIdToUnregister">The previous hot key ID associated with this action that should be unregistered</param>
        /// <returns>The ID associated with this hot key combination</returns>
        public int RegisterHotKeyToHotKeyId(IList<ModifierKeys> modifiers, Key key, Action callback, int? previousHotKeyIdToUnregister = null)
        {
            if (modifiers.Distinct().Count() != modifiers.Count)
            {
                throw new ArgumentException($"{nameof(modifiers)} must contain unique entries");
            }
            if (modifiers.Contains(ModifierKeys.Windows))
            {
                throw new ArgumentException("Windows modifier key is not supported for hot keys");
            }

            var id = NextUpHotKeyId;

            if (previousHotKeyIdToUnregister.HasValue)
            {
                if (!UnregisterHotKey(previousHotKeyIdToUnregister.Value))
                {
                    throw new ApplicationException("Unable to unregister previous hot key.");
                }
            }

            if (!RegisterHotKey(_source.Handle, id, (uint) modifiers.Sum(modifier => (int) modifier),
                (uint) KeyInterop.VirtualKeyFromKey(key)))
            {
                throw new ApplicationException("Unable to register hot key combination of " +
                    $"{string.Join("+",modifiers.Select(modifier => modifier.ToString()))}+{key}.");
            }

            _hotKeyIdActionMapping.Add(id, callback);

            return id;
        }

        /// <summary>
        /// Unregister the hot key with the given ID
        /// </summary>
        /// <param name="previousHotKeyIdToUnregister"></param>
        /// <returns></returns>
        private bool UnregisterHotKey(int previousHotKeyIdToUnregister)
        {
            if (UnregisterHotKey(_source.Handle, previousHotKeyIdToUnregister))
            {
                _hotKeyIdActionMapping.Remove(previousHotKeyIdToUnregister);
                return true;
            }

            return false;

        }
    }
}
