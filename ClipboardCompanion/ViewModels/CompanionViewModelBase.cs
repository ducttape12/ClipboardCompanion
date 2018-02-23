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
    public abstract class CompanionViewModelBase<T> : ICompanionViewModel where T:BaseCompanionModel, new()
    {
        protected IPersistence<T> Persistence { get; }
        protected INotificationService NotificationService { get; }
        private readonly IHotKeyService _hotKeyService;
        private HotKeyBinding _hotKey;
        public event PropertyChangedEventHandler PropertyChanged;

        public abstract Action HotKeyPressedAction { get; }
        
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected CompanionViewModelBase(IHotKeyService hotKeyService, IPersistence<T> persistence, INotificationService notificationService)
        {
            Persistence = persistence;
            NotificationService = notificationService;
            _hotKeyService = hotKeyService;
            _hotKeyService.Initialized += (sender, eventArgs) => UpdateHotKeyHandling();
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
            get => Persistence.Load().IsEnabled;
            set
            {
                var model = Persistence.Load();
                model.IsEnabled = value;
                Persistence.Save(model);

                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(IsEnabled));
            }
        }

        public bool Bound
        {
            get => IsEnabled;
            set
            {
                IsEnabled = value;
                RaisePropertyChanged(nameof(Bound));
                RaisePropertyChanged(nameof(Unbound));
            }
        }

        public bool Unbound
        {
            get => !IsEnabled;
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
            get => Persistence.Load().ControlModifier;
            set
            {
                var model = Persistence.Load();
                model.ControlModifier = value;
                Persistence.Save(model);

                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(ControlModifier));
                RaisePropertyChanged(nameof(HotKeyDescription));
            }
        }

        public bool AltModifier
        {
            get => Persistence.Load().AltModifier;
            set
            {
                var model = Persistence.Load();
                model.AltModifier = value;
                Persistence.Save(model);

                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(AltModifier));
                RaisePropertyChanged(nameof(HotKeyDescription));
            }
        }

        public bool ShiftModifier
        {
            get => Persistence.Load().ShiftModifier;
            set
            {
                var model = Persistence.Load();
                model.ShiftModifier = value;
                Persistence.Save(model);

                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(ShiftModifier));
                RaisePropertyChanged(nameof(HotKeyDescription));
            }
        }

        public Key Key
        {
            get => Persistence.Load().Key;
            set
            {
                var model = Persistence.Load();
                model.Key = value;
                Persistence.Save(model);

                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(Key));
                RaisePropertyChanged(nameof(HotKeyDescription));
            }
        }

        private void UpdateHotKeyHandling()
        {
            if (IsEnabled)
            {
                RegisterHotKey();
            }
            else
            {
                UnregisterHotKey();
            }
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
    }
}