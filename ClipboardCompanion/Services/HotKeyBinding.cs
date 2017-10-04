using System;

namespace ClipboardCompanion.Services
{
    public class HotKeyBinding
    {
        public int Id { get; set; }
        public Action OnHotKeyPressed { get; set; }
    }
}
