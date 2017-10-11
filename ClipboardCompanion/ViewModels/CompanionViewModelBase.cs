using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{
    public abstract class CompanionViewModelBase : INotifyPropertyChanged
    {
        private readonly IHotKeyService _hotKeyService;
        private bool _isEnabled;
        private bool _controlModifier;
        private bool _altModifier;
        private bool _shiftModifier;
        private Key _key;
        private HotKeyBinding _hotKey;
        private bool _isInitiailized;

        protected CompanionViewModelBase(IHotKeyService hotKeyService)
        {
            _hotKeyService = hotKeyService;
        }

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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
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
        }

        private void UnregisterHotKey()
        {
            if (!_isInitiailized) return;

            if (_hotKey != null)
            {
                _hotKeyService.UnregisterHotKey(_hotKey);
            }
        }

        private void RegisterHotKey()
        {
            if (!_isInitiailized) return;

            UnregisterHotKey();

            var modifiers = new List<ModifierKeys>();
            if (ControlModifier) modifiers.Add(ModifierKeys.Control);
            if (AltModifier) modifiers.Add(ModifierKeys.Alt);
            if (ShiftModifier) modifiers.Add(ModifierKeys.Shift);

            _hotKey = _hotKeyService.RegisterHotKey(modifiers, Key);
            _hotKey.OnHotKeyPressed = HotKeyPressedAction;
        }

        public abstract Action HotKeyPressedAction { get; }

        public virtual void Initialize()
        {
            _isInitiailized = true;

            UpdateHotKeyHandling();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}