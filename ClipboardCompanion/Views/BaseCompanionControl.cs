using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public abstract class BaseCompanionControl : UserControl, INotifyPropertyChanged
    {
        private readonly ICompanionViewModel _viewModel;
        private bool _isInitialized;

        public abstract string Description { get; }
        public bool CompanionIsEnabled => _viewModel.IsEnabled;
        public event PropertyChangedEventHandler PropertyChanged;
        public string HotKeyDescription => _viewModel.HotKeyDescription;

        protected BaseCompanionControl(ICompanionViewModel viewModel)
        {
            _viewModel = viewModel;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!_isInitialized)
            {
                _viewModel.Initialize();
                DataContext = _viewModel;
                _viewModel.PropertyChanged += _viewModel_PropertyChanged;
                _isInitialized = true;
            }
        }

        private void _viewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_viewModel.IsEnabled))
            {
                OnPropertyChanged(nameof(CompanionIsEnabled));
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
