using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    /// <summary>
    /// Interaction logic for XmlFormatterCompanionControl.xaml
    /// </summary>
    public partial class XmlFormatterCompanionControl : BaseCompanionControl
    {
        public XmlFormatterCompanionControl(XmlFormatterCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "XML Formatter";
    }
}
