using System;
using System.Windows;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
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

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistence, INotificationService notificationService) :
            base(hotKeyService, persistence, notificationService, persistence.TextCleanerCompanionModel)
        {
            Trim = Persistence.TextCleanerCompanionModel.Trim;
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

                        NotificationService.ShowMessage("Cleared clipboard text's formatting.");
                    }
                };

        protected override void SaveConfiguration()
        {
            Persistence.Save(new TextCleanerCompanionModel
            {
                IsEnabled = IsEnabled,
                ShiftModifier = ShiftModifier,
                ControlModifier = ControlModifier,
                Key = Key,
                Trim = Trim
            });
        }
    }
}
