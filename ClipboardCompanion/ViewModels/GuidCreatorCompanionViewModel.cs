using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClipboardCompanion.Enums;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class GuidCreatorCompanionViewModel : CompanionViewModelBase<GuidCreatorCompanionModel>
    {
        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService, INotificationService notificationService,
            IPersistence<GuidCreatorCompanionModel> persistence) : base(hotKeyService, persistence, notificationService)
        {
        }
        
        public GuidCasing Casing
        {
            get => Persistence.Load().Casing;
            set
            {
                var model = Persistence.Load();
                model.Casing = value;
                Persistence.Save(model);
                
                RaisePropertyChanged(nameof(Casing));
            }
        }

        public ObservableCollection<GuidCasing> GuidCasingOptions { get; } =
            new ObservableCollection<GuidCasing>(Enum.GetValues(typeof(GuidCasing)).Cast<GuidCasing>());
        
        public GuidStyle Style
        {
            get => Persistence.Load().Style;
            set
            {
                var model = Persistence.Load();
                model.Style = value;
                Persistence.Save(model);
                
                RaisePropertyChanged(nameof(Style));
            }
        }
        public ObservableCollection<GuidStyle> GuidStyleOptions { get; } =
            new ObservableCollection<GuidStyle>(Enum.GetValues(typeof(GuidStyle)).Cast<GuidStyle>());

        public override Action HotKeyPressedAction =>
            () =>
                {
                    var guidFormat = DetermineGuidFormatter(Style);
                    var guid = Guid.NewGuid().ToString(guidFormat);
                    var casedGuid = CaseGuid(guid, Casing);

                    Clipboard.SetText(casedGuid);

                    NotificationService.ShowMessage($"GUID {casedGuid} has been placed on the clipboard.");
                };

        private static string CaseGuid(string guid, GuidCasing casing)
        {
            switch (casing)
            {
                case GuidCasing.LowerCase:
                    return guid.ToLowerInvariant();
                case GuidCasing.UpperCase:
                    return guid.ToUpperInvariant();
                default:
                    throw new NotImplementedException($"Unknown GUID casing {casing}");
            }
        }

        private static string DetermineGuidFormatter(GuidStyle style)
        {
            switch (style)
            {
                case GuidStyle.Plain:
                    return "N";
                case GuidStyle.Hyphens:
                    return "D";
                case GuidStyle.HyphensBraces:
                    return "B";
                case GuidStyle.HyhpensParentheses:
                    return "P";
                default:
                    throw new NotImplementedException($"Unknown GuidStyle {style}");
            }
        }
    }
}
