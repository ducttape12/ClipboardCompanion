using System;
using System.Windows.Interop;

namespace ClipboardCompanion.Services
{
    public class WindowHandleService : IWindowHandleService
    {
        private HwndSource _source;

        public void RegisterWindowHandle(HwndSource source)
        {
            _source = source;
        }

        public void AddHookToRegisteredHandle(HwndSourceHook hook)
        {
            _source?.AddHook(hook);
        }

        public IntPtr Handle => _source?.Handle ?? IntPtr.Zero;
    }
}
