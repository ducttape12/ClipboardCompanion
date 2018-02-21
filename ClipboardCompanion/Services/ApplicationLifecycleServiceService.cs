using System.Windows;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.Services
{
    public class ApplicationLifecycleServiceService : IApplicationLifecycleService
    {
        public void Shutdown()
        {
            Application.Current.Shutdown();
        }
    }
}
