using System;
using System.Windows;
using System.Windows.Forms;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.Services
{
    public class TrayIconService : ITrayIconService
    {
        private Window _window;
        private readonly NotifyIcon _trayIcon;
        private WindowState _previousWindowState;

        public TrayIconService()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = Properties.Resources.ClipboardCompanion
            };
            _trayIcon.MouseClick += TrayIconOnMouseClick;
        }

        private void TrayIconOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.Button == MouseButtons.Left)
            {
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

        public void Dispose()
        {
            _trayIcon?.Dispose();
        }
    }
}
