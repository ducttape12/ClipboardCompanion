using System.Windows.Input;

namespace ClipboardCompanion.Persistence.Models
{
    public class XmlFormatterCompanionModel : BaseCompanionModel
    {
        public bool XmlDeclaration { get; set; }
        public bool AttributesOnSeparateLines { get; set; }

        public XmlFormatterCompanionModel()
        {
            IsEnabled = false;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.X;
            XmlDeclaration = false;
            AttributesOnSeparateLines = false;
        }
    }
}
