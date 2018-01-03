using System.Windows;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public class BaseCompanionControl : UserControl
    {
        protected IInitializeViewModel ViewModel { get; }

        private bool _initialized;
        
        protected BaseCompanionControl(IInitializeViewModel viewModel)
        {
            Loaded += OnLoaded;
            ViewModel = viewModel;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!_initialized)
            {
                ViewModel.Initialize();
                DataContext = ViewModel;
                _initialized = true;
            }
        }
    }
}
