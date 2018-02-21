using System;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface ITrayIconService : IDisposable
    {
        void RegisterWindow(System.Windows.Window window);
        bool AlwaysShowTrayIcon { get; set; }
        bool MinimizeToTray { get; set; }
        bool StartMinimized { get; set; }
    }
}
