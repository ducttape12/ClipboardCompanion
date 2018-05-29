using System.Windows.Input;
using ClipboardCompanion.Enums;

namespace ClipboardCompanion.Persistence.Models
{
    public class GuidCreatorCompanionModel : BaseCompanionModel
    {
        public GuidCasing Casing { get; set; }
        public GuidStyle Style { get; set; }

        public GuidCreatorCompanionModel()
        {
            IsEnabled = false;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.G;
            Casing = GuidCasing.LowerCase;
            Style = GuidStyle.HyphensBraces;
        }
    }
}
