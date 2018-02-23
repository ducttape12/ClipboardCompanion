using System;
using System.ComponentModel;

namespace ClipboardCompanion.ViewModels.Interfaces
{
    public interface ICompanionViewModel : INotifyPropertyChanged
    {
        bool IsEnabled { get; }
        string HotKeyDescription { get; }
        bool ValidHotKey { get; }
    }
}
