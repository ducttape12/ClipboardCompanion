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
            _trayIcon.Click += TrayIcon_Click;
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            if (_window.WindowState == WindowState.Minimized)
            {
                _window.Show();
                _window.WindowState = _previousWindowState;
            }
            else
            {
                _window.WindowState = WindowState.Minimized;
            }

        }

        public void Register(Window window)
        {
            _window = window;
            _previousWindowState = _window.WindowState;
            _window.StateChanged += WindowOnStateChanged;
        }

        private void WindowOnStateChanged(object sender, EventArgs eventArgs)
        {
            if (_window.WindowState == WindowState.Minimized && MinimizeToTray)
            {
                _window.Hide();
                _trayIcon.Visible = true;
            }
            else
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
    }
}
