using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels;
using ClipboardCompanion.Views;

namespace ClipboardCompanion
{
    public partial class MainWindow : Window
    {
        private readonly IHotKeyService _hotKeyService;
        private readonly ITrayIconService _trayIconService;
        private readonly CompanionSelector _companionSelector;
        private readonly MainWindowViewModel _mainWindowViewModel;
        private readonly IApplicationLifecycleService _applicationLifecycleService;

        public MainWindow(IHotKeyService hotKeyService, ITrayIconService trayIconService,
            CompanionSelector companionSelector, MainWindowViewModel mainWindowViewModel,
            IApplicationLifecycleService applicationLifecycleService)
        {
            InitializeComponent();
            _hotKeyService = hotKeyService;
            _trayIconService = trayIconService;
            _companionSelector = companionSelector;
            _mainWindowViewModel = mainWindowViewModel;
            _applicationLifecycleService = applicationLifecycleService;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            _hotKeyService.RegisterWindowHandle(hwndSource);
            _trayIconService.RegisterWindow(this);

            DockPanel.SetDock(_companionSelector, Dock.Bottom);
            MenuAndContent.Children.Add(_companionSelector);

            DataContext = _mainWindowViewModel;
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            _trayIconService.Dispose();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _applicationLifecycleService.Shutdown();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            _trayIconService.Enabled = false;
            new About().ShowDialog();
            _trayIconService.Enabled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _applicationLifecycleService.Shutdown();
        }
    }
}
