using System.ComponentModel;
using ClipboardCompanion.Persistance.Interfaces;
using ClipboardCompanion.Persistance.Models;

namespace ClipboardCompanion.ViewModels
{
    public class OptionsViewModel : INotifyPropertyChanged
    {
        private readonly ICompanionPersistence _companionPersistence;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _minimizeToTray;
        public bool MinimizeToTray
        {
            get => _minimizeToTray;
            set
            {
                _minimizeToTray = value;
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
                SaveConfiguration();
                RaisePropertyChanged(nameof(StartMinimized));
            }
        }

        public OptionsViewModel(ICompanionPersistence companionPersistence)
        {
            _companionPersistence = companionPersistence;
        }

        private void SaveConfiguration()
        {
            _companionPersistence.Save(new OptionsModel
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
            var model = _companionPersistence.OptionsModel;

            AlwaysShowTrayIcon = model.AlwaysShowTrayIcon;
            MinimizeToTray = model.MinimizeToTray;
            StartMinimized = model.StartMinimized;
        }
    }
}
