using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public abstract class BaseCompanionControl : UserControl, INotifyPropertyChanged
    {
        private readonly ICompanionViewModel _viewModel;

        public abstract string Description { get; }
        public bool CompanionIsEnabled => _viewModel.IsEnabled;
        public event PropertyChangedEventHandler PropertyChanged;
        public string HotKeyDescription => _viewModel.HotKeyDescription;
        public bool ValidHotKey => _viewModel.ValidHotKey;

        protected BaseCompanionControl(ICompanionViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.IsEnabled):
                    RaisePropertyChanged(nameof(CompanionIsEnabled));
                    break;
                case nameof(_viewModel.HotKeyDescription):
                    RaisePropertyChanged(nameof(HotKeyDescription));
                    break;
                case nameof(_viewModel.ValidHotKey):
                    RaisePropertyChanged(nameof(ValidHotKey));
                    break;
            }
        }

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
