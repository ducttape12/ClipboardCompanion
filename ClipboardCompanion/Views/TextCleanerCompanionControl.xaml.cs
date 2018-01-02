using ClipboardCompanion.ViewModels;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public partial class TextCleanerCompanionControl : BaseCompanionControl
    {
        private readonly TextCleanerCompanionViewModel _companionViewModel;
        protected override IInitializeViewModel ViewModel => _companionViewModel;

        public TextCleanerCompanionControl(TextCleanerCompanionViewModel companionViewModel)
        {
            _companionViewModel = companionViewModel;
            InitializeComponent();
        }
    }
}
