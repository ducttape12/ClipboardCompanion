using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public abstract class BaseCompanionControl : BaseUserControl
    {
        private readonly ICompanionViewModel _viewModel;

        protected BaseCompanionControl(ICompanionViewModel viewModel) : base(viewModel)
        {
            _viewModel = viewModel;
        }

        public abstract string Description { get; }

        public bool CompanionIsEnabled => _viewModel.IsEnabled;
    }
}
