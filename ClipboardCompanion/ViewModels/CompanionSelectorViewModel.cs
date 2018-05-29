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
            AddCompanionControl(guidCreatorControl);
            AddCompanionControl(textCleanerControl);
            AddCompanionControl(xmlFormatterCompanionControl);
            AddCompanionControl(jsonFormatterCompanionControl);

            SelectedUserControl = Companions.First();
        }

        private void AddCompanionControl(BaseCompanionControl companionControl)
        {
            companionControl.PropertyChanged += CompanionControlPropertyChanged;

            Companions.Add(companionControl);
        }

        private void CompanionControlPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BaseCompanionControl.HotKeyDescription))
            {
                var invalidCompanions = Companions.Where(companion => !companion.ValidHotKey);

                foreach (var companion in invalidCompanions)
                {
                    companion.ReRegisterHotKey();
                }
            }
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
