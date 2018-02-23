using System;
using System.Windows;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClipboardCompanion.ViewModels
{
    public class JsonFormatterCompanionViewModel : CompanionViewModelBase<JsonFormatterCompanionModel>
    {
        public JsonFormatterCompanionViewModel(IHotKeyService hotKeyService, IPersistence<JsonFormatterCompanionModel> persistence,
            INotificationService notificationService) : base(hotKeyService, persistence, notificationService)
        {
        }

        public override Action HotKeyPressedAction => FormatClipboardToJson;

        private void FormatClipboardToJson()
        {
            if (!Clipboard.ContainsText())
            {
                NotificationService.ShowWarning("No text on clipboard to format.");
                return;
            }

            try
            {
                var clipboardContents = Clipboard.GetText();
                var formatted = JToken.Parse(clipboardContents).ToString(Formatting.Indented);
                Clipboard.SetText(formatted);

                NotificationService.ShowMessage("JSON on clipboard has been formatted.");
            }
            catch
            {
                NotificationService.ShowError("Clipboard contains invalid JSON. Unable to format.");
            }
        }
    }
}
