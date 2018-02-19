using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClipboardCompanion.ViewModels.Interfaces;
using ClipboardCompanion.Views;

namespace ClipboardCompanion.ViewModels
{
    public class CompanionSelectorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BaseCompanionControl> Companions { get; } = new ObservableCollection<BaseCompanionControl>();

        private BaseCompanionControl _selectedUserControl;

        public BaseCompanionControl SelectedUserControl
        {
            get => _selectedUserControl;
            set
            {
                _selectedUserControl = value;
                OnPropertyChanged(nameof(SelectedUserControl));
            }
        }

        public CompanionSelectorViewModel(GuidCreatorControl guidCreatorControl,
            TextCleanerControl textCleanerControl,
            XmlFormatterCompanionControl xmlFormatterCompanionControl,
            JsonFormatterCompanionControl jsonFormatterCompanionControl)
        {
            Companions.Add(guidCreatorControl);
            Companions.Add(textCleanerControl);
            Companions.Add(xmlFormatterCompanionControl);
            Companions.Add(jsonFormatterCompanionControl);

            SelectedUserControl = Companions.First();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
        }
    }
}
