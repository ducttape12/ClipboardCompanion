using ClipboardCompanion.ViewModels;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public partial class GuidCreatorControl : BaseCompanionControl
    {
        private readonly GuidCreatorCompanionViewModel _companionViewModel;
        protected override IInitializeViewModel ViewModel => _companionViewModel;

        public GuidCreatorControl(GuidCreatorCompanionViewModel companionViewModel)
        {
            _companionViewModel = companionViewModel;
            InitializeComponent();
        }
    }
}
