using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class TextCleanerCompanionControl : BaseCompanionControl
    {
        public TextCleanerCompanionControl(TextCleanerCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }
    }
}
