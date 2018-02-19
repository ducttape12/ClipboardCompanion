using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Interop;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface IHotKeyService
    {
        HotKeyBinding RegisterHotKey(IList<ModifierKeys> modifierKeys, Key key);
        void UnregisterHotKey(HotKeyBinding hotKeyBinding);
        void RegisterWindowHandle(HwndSource source);
    }
}
