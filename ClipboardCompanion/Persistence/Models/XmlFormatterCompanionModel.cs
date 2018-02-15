using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClipboardCompanion.Persistence.Models
{
    public class XmlFormatterCompanionModel : BaseCompanionModel
    {
        public bool ForceXmlDeclaration { get; set; }
        public bool AttributesOnSeparateLines { get; set; }

        public XmlFormatterCompanionModel()
        {
            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.X;
            ForceXmlDeclaration = false;
            AttributesOnSeparateLines = false;
        }
    }
}
