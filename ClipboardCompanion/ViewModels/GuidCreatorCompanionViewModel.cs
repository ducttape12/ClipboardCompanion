﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ClipboardCompanion.Enums;
using ClipboardCompanion.Services.Interfaces;

namespace ClipboardCompanion.ViewModels
{

    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        private readonly INotificationService _notificationService;

        //public GuidCreatorCompanionViewModel() : base() { }

        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService, INotificationService notificationService) : base(hotKeyService)
        {
            _notificationService = notificationService;
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

        public override void Initialize()
        {
            IsEnabled = true;
            ShiftModifier = true;
            ControlModifier = true;
            Key = Key.G;

            Style = GuidStyle.HyhpensParentheses;
            Casing = GuidCasing.UpperCase;

            base.Initialize();
        }
    }
}
