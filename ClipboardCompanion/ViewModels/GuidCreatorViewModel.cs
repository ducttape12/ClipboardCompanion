using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{
    public class GuidCreatorViewModel : INotifyPropertyChanged
    {
        private readonly IHotKeyService _hotKeyService;

        private bool _isEnabled;
        private bool _controlModifier;
        private bool _altModifier;
        private bool _shiftModifier;
        private Key _key;
        private HotKeyBinding _hotKey;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled)));
            }
        }

        public bool ControlModifier
        {
            get => _controlModifier;
            set
            {
                _controlModifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ControlModifier)));
            }
        }

        public bool AltModifier
        {
            get => _altModifier;
            set
            {
                _altModifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AltModifier)));
            }
        }

        public bool ShiftModifier
        {
            get => _shiftModifier;
            set
            {
                _shiftModifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShiftModifier)));
            }
        }

        public Key Key
        {
            get => _key;
            set
            {
                _key = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuidCreatorViewModel(IHotKeyService hotKeyService)
        {
            _hotKeyService = hotKeyService;

            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = Key.G;
        }

        private void UnregisterHotKey()
        {
            if (_hotKey != null)
            {
                _hotKeyService.UnregisterHotKey(_hotKey);
            }
        }

        private void RegisterHotKey()
        {
            UnregisterHotKey();
            
            var modifiers = new List<ModifierKeys>();
            if(ControlModifier) modifiers.Add(ModifierKeys.Control);
            if(AltModifier) modifiers.Add(ModifierKeys.Alt);
            if(ShiftModifier) modifiers.Add(ModifierKeys.Shift);

            _hotKey = _hotKeyService.RegisterHotKey(modifiers, Key);
        }
    }
}
