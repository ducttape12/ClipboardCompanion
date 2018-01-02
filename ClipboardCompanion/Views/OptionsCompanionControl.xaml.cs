using ClipboardCompanion.ViewModels;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    /// <summary>
    /// Interaction logic for OptionsCompanionControl.xaml
    /// </summary>
    public partial class OptionsCompanionControl : BaseCompanionControl
    {
        private readonly OptionsCompanionViewModel _optionsCompanionViewModel;
        protected override IInitializeViewModel ViewModel => _optionsCompanionViewModel;

        public OptionsCompanionControl(OptionsCompanionViewModel optionsCompanionViewModel)
        {
            _optionsCompanionViewModel = optionsCompanionViewModel;
            InitializeComponent();
        }
    }
}
