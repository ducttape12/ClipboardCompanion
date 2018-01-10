using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class GuidCreatorControl : BaseCompanionControl
    {
        public GuidCreatorControl(GuidCreatorCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "GUID Creator";
    }
}
