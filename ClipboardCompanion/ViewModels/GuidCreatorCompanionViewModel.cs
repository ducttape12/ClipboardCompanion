using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ClipboardCompanion.Services;

namespace ClipboardCompanion.ViewModels
{

    public class GuidCreatorCompanionViewModel : CompanionViewModelBase
    {
        public enum GuidCasing
        {
            [Description("Lower Case")]
            LowerCase,
            [Description("Upper Case")]
            UpperCase
        }

        public enum GuidStyle
        {
            [Description("32 digits")]
            Plain,
            [Description("32 digits separated by hyphens")]
            Hyphens,
            [Description("32 digits separated by hyphens, enclosed in braces")]
            HyphensBraces,
            [Description("32 digits separated by hyphens, enclosed in parentheses")]
            HyhpensParentheses
        }

        //public GuidCreatorCompanionViewModel() : base() { }

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
