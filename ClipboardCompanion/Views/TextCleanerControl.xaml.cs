using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class TextCleanerControl : BaseCompanionControl
    {
        public TextCleanerControl(TextCleanerCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }

        public override string Description => "Text Cleaner";
    }
}
