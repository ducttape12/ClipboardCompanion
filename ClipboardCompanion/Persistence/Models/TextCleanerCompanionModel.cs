using System.Windows.Input;

namespace ClipboardCompanion.Persistence.Models
{
    public class TextCleanerCompanionModel : BaseCompanionModel
    {
        public bool Trim { get; set; }

        public TextCleanerCompanionModel()
        {
            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.C;
            Trim = true;
        }
    }
}
