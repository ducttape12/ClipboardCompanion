﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClipboardCompanion.Enums;
using ClipboardCompanion.Persistence.Interfaces;
using ClipboardCompanion.Persistence.Models;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{
    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService, INotificationService notificationService,
            IPersistence persistence) : base(hotKeyService, persistence, notificationService, persistence.GuidCreatorCompanionModel)
        {   
            Style = Persistence.GuidCreatorCompanionModel.Style;
            Casing = Persistence.GuidCreatorCompanionModel.Casing;
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

        protected override void SaveConfiguration()
        {
            Persistence.Save(new GuidCreatorCompanionModel
            {
                IsEnabled = IsEnabled,
                ShiftModifier = ShiftModifier,
                AltModifier = AltModifier,
                ControlModifier = ControlModifier,
                Key = Key,
                Style = Style,
                Casing = Casing
            });
        }
    }
}
