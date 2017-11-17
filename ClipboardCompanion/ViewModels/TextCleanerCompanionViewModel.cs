using System;
using System.Windows;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class TextCleanerCompanionViewModel : CompanionViewModelBase
    {
        private readonly IPersistence _persistance;
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

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistance) : base(hotKeyService)
        {
            _persistance = persistance;
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

        protected override void SaveConfiguration()
        {
            _persistance.Save(new TextCleanerCompanionModel
            {
                IsEnabled = IsEnabled,
                ShiftModifier = ShiftModifier,
                ControlModifier = ControlModifier,
                Key = Key,
                Trim = Trim
            });
        }

        public override void Initialize()
        {
            var model = _persistance.TextCleanerCompanionModel;

            IsEnabled = model.IsEnabled;
            ShiftModifier = model.ShiftModifier;
            ControlModifier = model.ControlModifier;
            Key = model.Key;

            Trim = model.Trim;

            base.Initialize();
        }
    }
}
