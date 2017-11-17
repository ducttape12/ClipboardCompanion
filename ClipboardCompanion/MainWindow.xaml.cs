using System.Windows;
using System.Windows.Interop;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels;

namespace ClipboardCompanion
{
    public partial class MainWindow : Window
    {
        private readonly IWindowHandleService _windowHandleService;
        private readonly MainWindowViewModel _viewModel;
        private readonly ITrayIconService _trayIconService;

        public MainWindow(IWindowHandleService windowHandleService, MainWindowViewModel viewModel, ITrayIconService trayIconService)
        {
            InitializeComponent();
            _windowHandleService = windowHandleService;
            _viewModel = viewModel;
            _trayIconService = trayIconService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            _windowHandleService.RegisterWindowHandle(hwndSource);

            _trayIconService.Register(this);
            DataContext = _viewModel;
        }
    }
}
