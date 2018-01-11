using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ClipboardCompanion.Services;
using ClipboardCompanion.ViewModels.Interfaces;
using ClipboardCompanion.Views;

namespace ClipboardCompanion.ViewModels
{
    public class CompanionSelectorViewModel : INotifyPropertyChanged, IInitializeViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<BaseCompanionControl> Companions { get; } = new ObservableCollection<BaseCompanionControl>();

        private BaseUserControl _selectedUserControl;

        public BaseUserControl SelectedUserControl
        {
            get => _selectedUserControl;
            set
            {
                _selectedUserControl = value;
                OnPropertyChanged(nameof(SelectedUserControl));
            }
        }

        //public CompanionSelectorViewModel() { }

        public CompanionSelectorViewModel(GuidCreatorControl guidCreatorControl,
            TextCleanerUserControl textCleanerUserControl)
        {
            Companions.Add(guidCreatorControl);
            Companions.Add(textCleanerUserControl);

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
