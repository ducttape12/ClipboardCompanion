﻿using System;

namespace ClipboardCompanion.Services.Interfaces
{
    public interface IWindowsHotKeyApiService
    {
        bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        bool UnregisterHotKey(IntPtr hWnd, int id);
    }
}
