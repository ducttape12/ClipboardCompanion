using System;
using System.Windows;
using ClipboardCompanion.Persistance.Interfaces;
using ClipboardCompanion.Persistance.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class TextCleanerCompanionViewModel : CompanionViewModelBase
    {
        private readonly ICompanionPersistance _companionPersistance;
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

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService, ICompanionPersistance companionPersistance) : base(hotKeyService)
        {
            _companionPersistance = companionPersistance;
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
            _companionPersistance.Save(new TextCleanerCompanionModel
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
            var model = _companionPersistance.TextCleanerCompanionModel;

            IsEnabled = model.IsEnabled;
            ShiftModifier = model.ShiftModifier;
            ControlModifier = model.ControlModifier;
            Key = model.Key;

            Trim = model.Trim;

            base.Initialize();
        }
    }
}
