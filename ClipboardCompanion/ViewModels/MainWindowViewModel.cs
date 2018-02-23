using System.ComponentModel;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly IPersistence<OptionsCompanionModel> _persistence;
        private readonly ITrayIconService _trayIconService;
        public event PropertyChangedEventHandler PropertyChanged;
        
        public bool MinimizeToTray
        {
            get => _persistence.Load().MinimizeToTray;
            set
            {
                var model = _persistence.Load();
                model.MinimizeToTray = value;
                _persistence.Save(model);

                SyncTrayIconService();
                RaisePropertyChanged(nameof(MinimizeToTray));
            }
        }
        
        public bool AlwaysShowTrayIcon
        {
            get => _persistence.Load().AlwaysShowTrayIcon;
            set
            {
                var model = _persistence.Load();
                model.AlwaysShowTrayIcon = value;
                _persistence.Save(model);

                SyncTrayIconService();
                RaisePropertyChanged(nameof(AlwaysShowTrayIcon));
            }
        }
        
        public bool StartMinimized
        {
            get => _persistence.Load().StartMinimized;
            set
            {
                var model = _persistence.Load();
                model.StartMinimized = value;
                _persistence.Save(model);

                SyncTrayIconService();
                RaisePropertyChanged(nameof(StartMinimized));
            }
        }

        public MainWindowViewModel(IPersistence<OptionsCompanionModel> persistence, ITrayIconService trayIconService)
        {
            _persistence = persistence;
            _trayIconService = trayIconService;

            SyncTrayIconService();
        }

        private void SyncTrayIconService()
        {
            _trayIconService.AlwaysShowTrayIcon = AlwaysShowTrayIcon;
            _trayIconService.MinimizeToTray = MinimizeToTray;
            _trayIconService.StartMinimized = StartMinimized;
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
