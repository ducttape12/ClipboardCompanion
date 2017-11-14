using System.Windows.Input;
using ClipboardCompanion.Enums;

namespace ClipboardCompanion.Persistance.Models
{
    public class GuidCreatorCompanionModel : BaseCompanionModel
    {
        public GuidCasing Casing { get; set; }
        public GuidStyle Style { get; set; }

        public GuidCreatorCompanionModel()
        {
            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.G;
            Casing = GuidCasing.LowerCase;
            Style = GuidStyle.HyphensBraces;
        }
    }
}
