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
        }

        public void Register(Window window)
        {
            _window = window;
            _window.StateChanged += WindowOnStateChanged;
        }

        private void WindowOnStateChanged(object sender, EventArgs eventArgs)
        {
            
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
