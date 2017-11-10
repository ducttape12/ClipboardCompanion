using System;
using System.Runtime.InteropServices;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.Services
{
    public class WindowsHotKeyApiService : IWindowsHotKeyApiService
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        bool IWindowsHotKeyApiService.RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk)
        {
            return RegisterHotKey(hWnd, id, fsModifiers, vk);
        }

        bool IWindowsHotKeyApiService.UnregisterHotKey(IntPtr hWnd, int id)
        {
            return UnregisterHotKey(hWnd, id);
        }
    }
}
