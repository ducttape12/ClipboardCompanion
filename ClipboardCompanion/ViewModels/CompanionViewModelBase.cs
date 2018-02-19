using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services;
using ClipboardCompanion.Services.Interfaces;
using ClipboardCompanion.ViewModels.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public abstract class CompanionViewModelBase : ICompanionViewModel
    {
        protected IPersistence Persistence { get; }
        protected INotificationService NotificationService { get; }
        private readonly IHotKeyService _hotKeyService;
        private bool _isEnabled;
        private bool _controlModifier;
        private bool _altModifier;
        private bool _shiftModifier;
        private Key _key;
        private HotKeyBinding _hotKey;

        protected CompanionViewModelBase(IHotKeyService hotKeyService, IPersistence persistence, INotificationService notificationService,
            BaseCompanionModel companionModel)
        {
            Persistence = persistence;
            NotificationService = notificationService;
            _hotKeyService = hotKeyService;
            _hotKeyService.Initialized += HotKeyServiceOnInitialized;
            
            IsEnabled = companionModel.IsEnabled;
            ShiftModifier = companionModel.ShiftModifier;
            ControlModifier = companionModel.ControlModifier;
            Key = companionModel.Key;
        }

        private void HotKeyServiceOnInitialized(object sender, EventArgs eventArgs)
        {
            UpdateHotKeyHandling();
        }

        public ObservableCollection<Key> ValidKeys { get; } = new ObservableCollection<Key>
                {
                    Key.Pause,
                    Key.CapsLock,
                    Key.Space,
                    Key.PageUp,
                    Key.Next,
                    Key.PageDown,
                    Key.End,
                    Key.Home,
                    Key.Left,
                    Key.Up,
                    Key.Right,
                    Key.Down,
                    Key.Select,
                    Key.Print,
                    Key.PrintScreen,
                    Key.Insert,
                    Key.Delete,
                    Key.Help,
                    Key.A,
                    Key.B,
                    Key.C,
                    Key.D,
                    Key.E,
                    Key.F,
                    Key.G,
                    Key.H,
                    Key.I,
                    Key.J,
                    Key.K,
                    Key.L,
                    Key.M,
                    Key.N,
                    Key.O,
                    Key.P,
                    Key.Q,
                    Key.R,
                    Key.S,
                    Key.T,
                    Key.U,
                    Key.V,
                    Key.W,
                    Key.X,
                    Key.Y,
                    Key.Z,
                    Key.NumPad0,
                    Key.NumPad1,
                    Key.NumPad2,
                    Key.NumPad3,
                    Key.NumPad4,
                    Key.NumPad5,
                    Key.NumPad6,
                    Key.NumPad7,
                    Key.NumPad8,
                    Key.NumPad9,
                    Key.Multiply,
                    Key.Add,
                    Key.Separator,
                    Key.Subtract,
                    Key.Decimal,
                    Key.Divide,
                    Key.F1,
                    Key.F2,
                    Key.F3,
                    Key.F4,
                    Key.F5,
                    Key.F6,
                    Key.F7,
                    Key.F8,
                    Key.F9,
                    Key.F10,
                    Key.F11,
                    Key.F12,
                    Key.F13,
                    Key.F14,
                    Key.F15,
                    Key.F16,
                    Key.F17,
                    Key.F18,
                    Key.F19,
                    Key.F20,
                    Key.F21,
                    Key.F22,
                    Key.F23,
                    Key.F24,
                    Key.NumLock,
                    Key.Scroll,
                    Key.VolumeMute,
                    Key.VolumeDown,
                    Key.VolumeUp,
                    Key.MediaNextTrack,
                    Key.MediaPreviousTrack,
                    Key.MediaStop,
                    Key.MediaPlayPause,
                    Key.LaunchMail,
                    Key.SelectMedia
                };

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        public bool Bound
        {
            get => _isEnabled;
            set
            {
                IsEnabled = value;
                RaisePropertyChanged(nameof(Bound));
                RaisePropertyChanged(nameof(Unbound));
            }
        }

        public bool Unbound
        {
            get => !_isEnabled;
            set
            {
                IsEnabled = !value;
                RaisePropertyChanged(nameof(Bound));
                RaisePropertyChanged(nameof(Unbound));
            }
        }

        public string HotKeyDescription
        {
            get
            {
                var control = ControlModifier ? "Ctrl+" : string.Empty;
                var alt = AltModifier ? "Alt+" : string.Empty;
                var shift = ShiftModifier ? "Shift+" : string.Empty;
                var key = Key.ToString();

                return IsEnabled ? $"{control}{alt}{shift}{key}" : string.Empty;
            }
        }

        public bool ControlModifier
        {
            get => _controlModifier;
            set
            {
                _controlModifier = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(ControlModifier));
            }
        }

        public bool AltModifier
        {
            get => _altModifier;
            set
            {
                _altModifier = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(AltModifier));
            }
        }

        public bool ShiftModifier
        {
            get => _shiftModifier;
            set
            {
                _shiftModifier = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(ShiftModifier));
            }
        }

        public Key Key
        {
            get => _key;
            set
            {
                _key = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(Key));
            }
        }

        protected void UpdateHotKeyHandling()
        {
            if (IsEnabled)
            {
                RegisterHotKey();
            }
            else
            {
                UnregisterHotKey();
            }

            SaveConfiguration();
        }

        private void RegisterHotKey()
        {
            if (!_hotKeyService.IsInitialized)
            {
                return;
            }

            UnregisterHotKey();

            var modifiers = new List<ModifierKeys>();
            if (ControlModifier) modifiers.Add(ModifierKeys.Control);
            if (AltModifier) modifiers.Add(ModifierKeys.Alt);
            if (ShiftModifier) modifiers.Add(ModifierKeys.Shift);

            _hotKey = _hotKeyService.RegisterHotKey(modifiers, Key);
            _hotKey.OnHotKeyPressed = HotKeyPressedAction;
        }

        private void UnregisterHotKey()
        {
            if (!_hotKeyService.IsInitialized)
            {
                return;
            }

            if (_hotKey != null)
            {
                _hotKeyService.UnregisterHotKey(_hotKey);
                _hotKey = null;
            }
        }

        public abstract Action HotKeyPressedAction { get; }

        protected abstract void SaveConfiguration();
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _hotKeyService.Initialized -= HotKeyServiceOnInitialized;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        ~CompanionViewModelBase()
        {
            Dispose(false);
        }
    }
}