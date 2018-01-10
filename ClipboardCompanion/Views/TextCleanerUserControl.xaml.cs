using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class TextCleanerUserControl : BaseCompanionControl
    {
        public TextCleanerUserControl(TextCleanerCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "Text Cleaner";
    }
}
