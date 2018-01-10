using System.Windows;
using System.Windows.Controls;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.Views
{
    public class BaseUserControl : UserControl
    {
        protected IInitializeViewModel ViewModel { get; }

        private bool _initialized;
        
        protected BaseUserControl(IInitializeViewModel viewModel)
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
