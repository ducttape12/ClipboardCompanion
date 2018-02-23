using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ClipboardCompanion.Services
{
    public class HotKeyBinding
    {
        public int Id { get; }
        public Action OnHotKeyPressed { get; set; }
        public IList<ModifierKeys> ModifierKeys { get; }
        public Key Key { get; }

        public HotKeyBinding(int id, Action onHotKeyPressed, IList<ModifierKeys> modifierKeys, Key key)
        {
            Id = id;
            OnHotKeyPressed = onHotKeyPressed;
            ModifierKeys = new ReadOnlyCollection<ModifierKeys>(modifierKeys);
            Key = key;
        }
    }
}
