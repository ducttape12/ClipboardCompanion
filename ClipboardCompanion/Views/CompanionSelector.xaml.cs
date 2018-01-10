using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class CompanionSelector : BaseUserControl
    {
        public CompanionSelector(CompanionSelectorViewModel companionSelectorViewModel) : base(companionSelectorViewModel)
        {
            InitializeComponent();
            DataContext = companionSelectorViewModel;
        }
    }
}
