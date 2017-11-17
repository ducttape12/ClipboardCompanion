using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface ITrayIconService
    {
        void Register(System.Windows.Window window);
        bool AlwaysShowTrayIcon { get; set; }
        bool MinimizeToTray { get; set; }
    }
}
