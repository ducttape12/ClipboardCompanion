using System;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class JsonFormatterCompanionViewModel : CompanionViewModelBase
    {
        private readonly IPersistence _persistance;
        private readonly INotificationService _notificationService;

        public JsonFormatterCompanionViewModel(IHotKeyService hotKeyService, IPersistence persistance, INotificationService notificationService) :
            base(hotKeyService, persistance.XmlFormatterCompanionModel)
        {
            _persistance = persistance;
            _notificationService = notificationService;
        }

        public override Action HotKeyPressedAction => () =>
        {
            // TODO: Implement JSON formatting logic here
            // var pretty = JToken.Parse(json).ToString(Formatting.Indented);
        };

        protected override void SaveConfiguration()
        {
            if (IsInitialized)
            {
                _persistance.Save(new XmlFormatterCompanionModel
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
