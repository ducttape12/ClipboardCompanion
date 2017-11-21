using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface ITrayIconService : IDisposable
    {
        void RegisterWindow(System.Windows.Window window);
        bool AlwaysShowTrayIcon { get; set; }
        bool MinimizeToTray { get; set; }
        bool StartMinimized { get; set; }
        void Initialize();
    }
}
