using System;
using System.Windows;
using System.Windows.Interop;
using ClipboardCompanion.Services;
using ClipboardCompanion.ViewModels;
using ClipboardCompanion.Views;

namespace ClipboardCompanion
{
    public partial class MainWindow : Window
    {
        private readonly IWindowHandleService _windowHandleService;
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(IWindowHandleService windowHandleService, MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _windowHandleService = windowHandleService;
            _viewModel = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            _windowHandleService.RegisterWindowHandle(hwndSource);

            DataContext = _viewModel;
            //Content = _guidCreatorControl;
        }
    }
}
