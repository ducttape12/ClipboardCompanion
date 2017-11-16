using System.Windows.Input;

namespace ClipboardCompanion.Persistance.Models
{
    public class BaseCompanionModel
    {
        public Key Key { get; set; }
        public bool ControlModifier { get; set; }
        public bool AltModifier { get; set; }
        public bool ShiftModifier { get; set; }
        public bool IsEnabled { get; set; }
    }
}
