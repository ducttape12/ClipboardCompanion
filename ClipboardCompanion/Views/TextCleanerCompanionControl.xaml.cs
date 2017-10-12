using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class TextCleanerCompanionControl : UserControl
    {
        private readonly TextCleanerCompanionViewModel _companionViewModel;

        public TextCleanerCompanionControl(TextCleanerCompanionViewModel companionViewModel)
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
