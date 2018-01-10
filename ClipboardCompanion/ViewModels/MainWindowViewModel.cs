using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using ClipboardCompanion.Views;

namespace ClipboardCompanion.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TabItem> CompanionTabItems { get; } = new ObservableCollection<TabItem>();

        //public MainWindowViewModel()
        //{
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel(GuidCreatorControl guidCreatorControl,
            TextCleanerUserControl textCleanerUserControl,
            OptionsUserControl optionsUserControl)
        {
            AddTabItem(guidCreatorControl, "GUID Creator");
            AddTabItem(textCleanerUserControl, "Text Cleaner");
            AddTabItem(optionsUserControl, "Options");

            SelectedTabItem = CompanionTabItems.First();
        }

        private void AddTabItem(UserControl userControl, string tabHeader)
        {
            var tabItem = new TabItem
            {
                Header = tabHeader,
                Content = userControl
            };

            CompanionTabItems.Add(tabItem);
        }

        private TabItem _selectedTabItem;
        public TabItem SelectedTabItem
        {
            get => _selectedTabItem;
            set
            {
                _selectedTabItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedTabItem)));
            }
        }

    }
}
