using System.ComponentModel;

namespace ClipboardCompanion.Enums
{
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
}
