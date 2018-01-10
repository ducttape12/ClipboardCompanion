using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public abstract class BaseCompanionControl : BaseUserControl
    {
        protected BaseCompanionControl(IInitializeViewModel viewModel) : base(viewModel)
        {
        }

        public abstract string Description { get; }
    }
}
