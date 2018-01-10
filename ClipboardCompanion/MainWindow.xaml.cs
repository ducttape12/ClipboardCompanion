using System.Windows;
using System.Windows.Interop;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels;
using ClipboardCompanion.Views;

namespace ClipboardCompanion
{
    public partial class MainWindow : Window
    {
        private readonly IWindowHandleService _windowHandleService;
        private readonly ITrayIconService _trayIconService;
        private readonly CompanionSelector _companionSelector;

        public MainWindow(IWindowHandleService windowHandleService, ITrayIconService trayIconService, CompanionSelector companionSelector)
        {
            InitializeComponent();
            _windowHandleService = windowHandleService;
            _trayIconService = trayIconService;
            _companionSelector = companionSelector;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            _windowHandleService.RegisterWindowHandle(hwndSource);
            _trayIconService.RegisterWindow(this);

            Content = _companionSelector;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            _trayIconService.Dispose();
        }
    }
}
