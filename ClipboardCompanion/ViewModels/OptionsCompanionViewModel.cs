﻿using System.ComponentModel;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class OptionsCompanionViewModel : INotifyPropertyChanged
    {
        private readonly IPersistence _persistence;
        private readonly ITrayIconService _trayIconService;
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _minimizeToTray;
        public bool MinimizeToTray
        {
            get => _minimizeToTray;
            set
            {
                _minimizeToTray = value;
                _trayIconService.MinimizeToTray = value;
                SaveConfiguration();
                RaisePropertyChanged(nameof(MinimizeToTray));
            }
        }

        private bool _alwaysShowTrayIcon;

        public bool AlwaysShowTrayIcon
        {
            get => _alwaysShowTrayIcon;
            set
            {
                _alwaysShowTrayIcon = value;
                _trayIconService.AlwaysShowTrayIcon = value;
                SaveConfiguration();
                RaisePropertyChanged(nameof(AlwaysShowTrayIcon));
            }
        }

        private bool _startMinimized;

        public bool StartMinimized
        {
            get => _startMinimized;
            set
            {
                _startMinimized = value;
                _trayIconService.StartMinimized = value;
                SaveConfiguration();
                RaisePropertyChanged(nameof(StartMinimized));
            }
        }

        public OptionsCompanionViewModel(IPersistence persistence, ITrayIconService trayIconService)
        {
            _persistence = persistence;
            _trayIconService = trayIconService;

            InitializeCompanionConfiguration();
        }

        private void InitializeCompanionConfiguration()
        {
            var model = _persistence.OptionsCompanionModel;

            AlwaysShowTrayIcon = model.AlwaysShowTrayIcon;
            MinimizeToTray = model.MinimizeToTray;
            StartMinimized = model.StartMinimized;
        }

        private void SaveConfiguration()
        {
            _persistence.Save(new OptionsCompanionModel
            {
                AlwaysShowTrayIcon = AlwaysShowTrayIcon,
                MinimizeToTray = MinimizeToTray,
                StartMinimized = StartMinimized
            });
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            _trayIconService.Initialize();
        }
    }
}