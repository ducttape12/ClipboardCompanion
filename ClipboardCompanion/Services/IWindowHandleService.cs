using System;
using System.Windows.Interop;

namespace ClipboardCompanion.Services
{
    public interface IWindowHandleService
    {
        void AddHookToRegisteredHandle(HwndSourceHook hook);
        IntPtr Handle { get; }
        void RegisterWindowHandle(HwndSource source);
    }
}