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
        private readonly GuidCreatorControl _guidCreatorControl;

        //public MainWindow()
        //{
        //    InitializeComponent();
        //    this.Content = new GuidCreatorControl(new GuidCreatorCompanionViewModel());
        //}

        public MainWindow(IWindowHandleService windowHandleService, GuidCreatorControl guidCreatorControl)
        {
            InitializeComponent();
            _windowHandleService = windowHandleService;
            _guidCreatorControl = guidCreatorControl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            _windowHandleService.RegisterWindowHandle(hwndSource);

            Content = _guidCreatorControl;
        }
    }
}
