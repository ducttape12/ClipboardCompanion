using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{
    public class GuidCreatorViewModel : INotifyPropertyChanged
    {
        private readonly HotKeyService _hotKeyService;

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;

                if (!_isEnabled)
                {

                }
            }
        }

        public bool ControlModifier { get; set; }
        public bool AltModifier { get; set; }
        public bool ShiftModifier { get; set; }
        public string Key { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public GuidCreatorViewModel(HotKeyService hotKeyService)
        {
            _hotKeyService = hotKeyService;

            IsEnabled = true;
            ControlModifier = true;
            ShiftModifier = true;
            Key = "G";
        }
    }
}
