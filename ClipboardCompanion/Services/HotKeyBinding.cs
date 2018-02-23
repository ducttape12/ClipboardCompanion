using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ClipboardCompanion.Services
{
    public class HotKeyBinding
    {
        public int Id { get; set; }
        public Action OnHotKeyPressed { get; set; }
        public IList<ModifierKeys> ModifierKeys { get; set; }
        public Key Key { get; set; }
    }
}
