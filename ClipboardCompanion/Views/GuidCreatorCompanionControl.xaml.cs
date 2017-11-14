using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class GuidCreatorControl : UserControl
    {
        private readonly GuidCreatorCompanionViewModel _companionViewModel;

        public GuidCreatorControl(GuidCreatorCompanionViewModel companionViewModel)
        {
            InitializeComponent();

            _companionViewModel = companionViewModel;
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _companionViewModel.Initialize();
            DataContext = _companionViewModel;
        }
    }
}
