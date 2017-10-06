using System.Collections.Generic;
using System.Windows.Input;

namespace ClipboardCompanion.Services
{
    public interface IHotKeyService
    {
        HotKeyBinding RegisterHotKey(IList<ModifierKeys> modifierKeys, Key key);
        void UnregisterHotKey(int id);
    }
}
