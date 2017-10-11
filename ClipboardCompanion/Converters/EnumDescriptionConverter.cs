using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace ClipboardCompanion.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var field = value.GetType().GetField(value.ToString());

            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;

            //var enumString = value.ToString();
            //var camelCaseString = Regex.Replace(enumString, "([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", "$1 ").ToLower();
            //return char.ToUpper(camelCaseString[0]) + camelCaseString.Substring(1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
