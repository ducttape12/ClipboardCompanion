using System;
using System.Windows;
using System.Windows.Input;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{
    public enum GuidCasing
    {
        LowerCase,
        UpperCase
    }

    public enum GuidStyle
    {
        Plain,
        Hyphens,
        HyphensBraces,
        HyhpensParentheses
    }

    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        public GuidCreatorCompanionViewModel(IHotKeyService hotKeyService) : base(hotKeyService)
        {
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

        public override Action HotKeyPressedAction
        {
            get
            {
                return () =>
                {
                    var guidFormat = DetermineGuidFormatter(Style);
                    var guid = Guid.NewGuid().ToString(guidFormat);
                    var casedGuid = CaseGuid(guid, Casing);

                    Clipboard.SetText(casedGuid);
                };
            }
        }

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
