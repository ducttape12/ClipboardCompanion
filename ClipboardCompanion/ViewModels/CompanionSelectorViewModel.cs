using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ClipboardCompanion.Services;
using ClipboardCompanion.Views;

namespace ClipboardCompanion.ViewModels
{
    public class CompanionSelectorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<BaseCompanionControl> Companions { get; } = new ObservableCollection<BaseCompanionControl>();

        private BaseCompanionControl _selectedCompanionControl;

        public BaseCompanionControl SelectedCompanionControl
        {
            get => _selectedCompanionControl;
            set
            {
                _selectedCompanionControl = value;
                OnPropertyChanged(nameof(SelectedCompanionControl));
            }
        }

        public CompanionSelectorViewModel()
        {
            Companions.Add(new GuidCreatorControl(new GuidCreatorCompanionViewModel(null, null, null)));
        }

        //public CompanionSelectorViewModel(GuidCreatorControl guidCreatorControl,
        //    TextCleanerCompanionControl textCleanerCompanionControl)
        //{
        //    Companions.Add(guidCreatorControl);
        //    Companions.Add(textCleanerCompanionControl);
        //}

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
