using System.Windows.Forms;
using SimpleInjector.Diagnostics;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface INotificationService
    {
        void ShowMessage(string message);
        void ShowError(string message);
        void ShowWarning(string message);
    }
}