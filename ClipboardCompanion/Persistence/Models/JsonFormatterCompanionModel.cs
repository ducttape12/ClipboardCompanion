using System.Windows.Input;

namespace ClipboardCompanion.Persistence.Models
{
    public class JsonFormatterCompanionModel : BaseCompanionModel
    {
        public JsonFormatterCompanionModel()
        {
            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.J;
        }
    }
}
