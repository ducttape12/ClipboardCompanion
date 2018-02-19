using System;
using System.Windows;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClipboardCompanion.ViewModels
{
    public class JsonFormatterCompanionViewModel : CompanionViewModelBase
    {
        private readonly IPersistence _persistance;
        private readonly INotificationService _notificationService;

        public JsonFormatterCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistance, INotificationService notificationService) :
            base(hotKeyService, persistance.JsonFormatterCompanionModel)
        {
            _persistance = persistance;
            _notificationService = notificationService;
        }

        public override Action HotKeyPressedAction => FormatClipboardToJson;

        private void FormatClipboardToJson()
        {
            if (!Clipboard.ContainsText())
            {
                _notificationService.ShowWarning("No text on clipboard to format.");
                return;
            }

            try
            {
                var clipboardContents = Clipboard.GetText();
                var formatted = JToken.Parse(clipboardContents).ToString(Formatting.Indented);
                Clipboard.SetText(formatted);

                _notificationService.ShowMessage("JSON on clipboard has been formatted.");
            }
            catch
            {
                _notificationService.ShowError("Clipboard contains invalid JSON. Unable to format.");
            }
        }

        protected override void SaveConfiguration()
        {
            if (IsInitialized)
            {
                _persistance.Save(new JsonFormatterCompanionModel
                {
                    IsEnabled = IsEnabled,
                    ShiftModifier = ShiftModifier,
                    ControlModifier = ControlModifier,
                    Key = Key
                });
            }
        }
    }
}
