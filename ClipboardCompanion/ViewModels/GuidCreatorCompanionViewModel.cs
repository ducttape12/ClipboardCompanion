using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClipboardCompanion.Enums;
using ClipboardCompanion.Persistance.Interfaces;
using ClipboardCompanion.Persistance.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{

    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        private readonly INotificationService _notificationService;
        private readonly ICompanionPersistance _companionPersistance;

        //public GuidCreatorCompanionViewModel() : base() { }

        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService, INotificationService notificationService,
            ICompanionPersistance companionPersistance) : base(hotKeyService)
        {
            _notificationService = notificationService;
            _companionPersistance = companionPersistance;
        }

        private GuidCasing _casing;

        public GuidCasing Casing
        {
            get => _casing;
            set
            {
                _casing = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(Casing));
                SaveConfiguration();
            }
        }

        public ObservableCollection<GuidCasing> GuidCasingOptions { get; } =
            new ObservableCollection<GuidCasing>(Enum.GetValues(typeof(GuidCasing)).Cast<GuidCasing>());

        private GuidStyle _style;

        public GuidStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                UpdateHotKeyHandling();
                RaisePropertyChanged(nameof(Style));
                SaveConfiguration();
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

                    _notificationService.ShowNotification("A new GUID has been placed on the clipboard.");
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

        protected override void SaveConfiguration()
        {
            _companionPersistance.Save(new GuidCreatorCompanionModel
            {
                IsEnabled = IsEnabled,
                ShiftModifier = ShiftModifier,
                ControlModifier = ControlModifier,
                Key = Key,
                Style = Style,
                Casing = Casing
            });
        }

        public override void Initialize()
        {
            var model = _companionPersistance.GuidCreatorCompanionModel;

            IsEnabled = model.IsEnabled;
            ShiftModifier = model.ShiftModifier;
            ControlModifier = model.ControlModifier;
            Key = model.Key;

            Style = model.Style;
            Casing = model.Casing;

            base.Initialize();
        }
    }
}
