using System.ComponentModel;

namespace ClipboardCompanion.ViewModels.Interfaces
{
    public interface ICompanionViewModel : IInitializeViewModel, INotifyPropertyChanged
    {
        bool IsEnabled { get; }
    }
}
