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
        private readonly INotificationService _notificationService;
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

        public TextCleanerCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistance, INotificationService notificationService) :
            base(hotKeyService, persistance.TextCleanerCompanionModel)
        {
            _persistance = persistance;
            _notificationService = notificationService;

            InitializeCompanionConfiguration();
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

                        _notificationService.ShowNotification("Cleared clipboard text's formatting.");
                    }
                };

        protected override void SaveConfiguration()
        {
            if (IsInitialized)
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
        }

        private void InitializeCompanionConfiguration()
        {
            var model = _persistance.TextCleanerCompanionModel;

            Trim = model.Trim;
        }
    }
}
