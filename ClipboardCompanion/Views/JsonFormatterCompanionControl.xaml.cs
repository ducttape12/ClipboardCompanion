using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    /// <summary>
    /// Interaction logic for JsonFormatterCompanionControl.xaml
    /// </summary>
    public partial class JsonFormatterCompanionControl : BaseCompanionControl
    {
        public JsonFormatterCompanionControl(JsonFormatterCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "JSON Formatter";
    }
}
