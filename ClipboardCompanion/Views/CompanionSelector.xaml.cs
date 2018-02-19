using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class CompanionSelector : UserControl
    {
        public CompanionSelector(CompanionSelectorViewModel companionSelectorViewModel)
        {
            InitializeComponent();
            DataContext = companionSelectorViewModel;
        }
    }
}
