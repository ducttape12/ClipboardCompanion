using System.ComponentModel;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        private readonly IPersistence _persistence;
        private readonly ITrayIconService _trayIconService;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _minimizeToTray;
        public bool MinimizeToTray
        {
            get => _minimizeToTray;
            set
            {
                _minimizeToTray = value;
                _trayIconService.MinimizeToTray = value;
                SaveConfiguration();
                RaisePropertyChanged(nameof(MinimizeToTray));
            }
        }

        private bool _alwaysShowTrayIcon;

        public bool AlwaysShowTrayIcon
        {
            get => _alwaysShowTrayIcon;
            set
            {
                _alwaysShowTrayIcon = value;
                _trayIconService.AlwaysShowTrayIcon = value;
                SaveConfiguration();
                RaisePropertyChanged(nameof(AlwaysShowTrayIcon));
            }
        }

        private bool _startMinimized;

        public bool StartMinimized
        {
            get => _startMinimized;
            set
            {
                _startMinimized = value;
                // TODO: What to do about this?
                SaveConfiguration();
                RaisePropertyChanged(nameof(StartMinimized));
            }
        }

        public OptionsViewModel(IPersistence persistence, ITrayIconService trayIconService)
        {
            _persistence = persistence;
            _trayIconService = trayIconService;
        }

        private void SaveConfiguration()
        {
            _persistence.Save(new OptionsModel
            {
                AlwaysShowTrayIcon = AlwaysShowTrayIcon,
                MinimizeToTray = MinimizeToTray,
                StartMinimized = StartMinimized
            });
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            var model = _persistence.OptionsModel;

            AlwaysShowTrayIcon = model.AlwaysShowTrayIcon;
            MinimizeToTray = model.MinimizeToTray;
            StartMinimized = model.StartMinimized;
        }
    }
}
