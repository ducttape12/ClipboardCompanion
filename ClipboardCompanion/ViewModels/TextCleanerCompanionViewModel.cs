using System;
using System.Windows;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class TextCleanerCompanionViewModel : CompanionViewModelBase<TextCleanerCompanionModel>
    {
        public bool Trim
        {
            get => Persistence.Load().Trim;
            set
            {
                var model = Persistence.Load();
                model.Trim = value;
                Persistence.Save(model);
                
                RaisePropertyChanged(nameof(Trim));
            }
        }

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService, IPersistence<TextCleanerCompanionModel> persistence,
            INotificationService notificationService) : base(hotKeyService, persistence, notificationService)
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

                        NotificationService.ShowMessage("Cleared clipboard text's formatting.");
                    }
                };
    }
}
