using System;
using System.Windows;
using System.Windows.Forms;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.Services
{
    public class TrayIconService : ITrayIconService
    {
        private readonly IApplicationLifecycleService _applicationLifecycleService;
        private Window _window;
        private readonly NotifyIcon _trayIcon;
        private WindowState _previousWindowState;

        public TrayIconService(IApplicationLifecycleService applicationLifecycleService)
        {
            _applicationLifecycleService = applicationLifecycleService;

            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.ClipboardCompanion
            };
            _trayIcon.MouseClick += TrayIconOnMouseClick;

            InitializeContextMenu();
        }

        private void InitializeContextMenu()
        {
            var contextMenu = new ContextMenu();
            var exitMenuItem = new MenuItem
            {
                Index = 0,
                Text = "E&xit"
            };
            exitMenuItem.Click += ExitMenuItem_Click;
            contextMenu.MenuItems.AddRange(new[] { exitMenuItem });

            _trayIcon.ContextMenu = contextMenu;
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            _applicationLifecycleService.Shutdown();
        }

        private void TrayIconOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.Button == MouseButtons.Left)
            {
                OnLeftMouseClick();
            }
        }

        private void OnLeftMouseClick()
        {
            if (!Enabled)
            {
                return;
            }

            if (_window.WindowState == WindowState.Minimized)
            {
                _window.Show();
                _window.WindowState = _previousWindowState;
                _trayIcon.Visible = AlwaysShowTrayIcon;
            }
            else
            {
                _window.WindowState = WindowState.Minimized;
            }
        }

        public void RegisterWindow(Window window)
        {
            _window = window;
            _previousWindowState = _window.WindowState;
            _window.StateChanged += WindowOnStateChanged;

            if (StartMinimized)
            {
                _window.WindowState = WindowState.Minimized;
            }
        }

        private void WindowOnStateChanged(object sender, EventArgs eventArgs)
        {
            if (_window.WindowState == WindowState.Minimized && MinimizeToTray)
            {
                _window.Hide();
                _trayIcon.Visible = true;
            }
            else if(_window.WindowState != WindowState.Minimized)
            {
                _previousWindowState = _window.WindowState;
            }
        }

        private bool _alwaysShowTrayIcon;
        public bool AlwaysShowTrayIcon
        {
            get => _alwaysShowTrayIcon;
            set
            {
                _alwaysShowTrayIcon = value;
                _trayIcon.Visible = AlwaysShowTrayIcon;
            }
        }
        public bool MinimizeToTray { get; set; }
        public bool StartMinimized { get; set; }

        private bool _enabled;
        public bool Enabled 
        {
            get => _enabled;
            set
            {
                _enabled = value;

                foreach (MenuItem menuItem in _trayIcon.ContextMenu.MenuItems)
                {
                    menuItem.Enabled = _enabled;
                }
            }
        }

        public void Dispose()
        {
            _trayIcon?.Dispose();
        }
    }
}
