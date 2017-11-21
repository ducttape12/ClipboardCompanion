using System.Windows.Controls;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion.Views
{
    public partial class GuidCreatorControl : BaseCompanionControl
    {
        public GuidCreatorControl(GuidCreatorCompanionViewModel companionViewModel) : base(companionViewModel)
        {
            InitializeComponent();
        }
    }
}
