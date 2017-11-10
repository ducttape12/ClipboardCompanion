using System;
using System.Windows;
using System.Windows.Input;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class TextCleanerCompanionViewModel : CompanionViewModelBase
    {
        private bool _trim;

        public bool Trim
        {
            get => _trim;
            set
            {
                _trim = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(Trim));
            }
        }

        //public TextCleanerCompanionViewModel() { }

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService) : base(hotKeyService)
        {
        }

        public override Action HotKeyPressedAction =>
            () =>
                {
                    if (Clipboard.ContainsText())
                    {
                        var text = Clipboard.GetText();

                        if (Trim)
                        {
                            text = text.Trim();
                        }   

                        Clipboard.SetText(text);
                    }
                };

        public override void Initialize()
        {
            IsEnabled = true;
            ShiftModifier = true;
            ControlModifier = true;
            Key = Key.C;

            Trim = true;

            base.Initialize();
        }
    }
}
