using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace ClipboardCompanion.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var arrayOfValues = value as IEnumerable;

            if (arrayOfValues == null)
            {
                return GetDescriptionAttribute(value);
            }

            var translatedValues = new List<string>();
            foreach (var element in arrayOfValues)
            {
                translatedValues.Add(GetDescriptionAttribute(element));
            }
            return translatedValues;
        }

        private static string GetDescriptionAttribute(object value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var valueString = value as string;

            if (valueString == null)
            {
                return value;
            }

            var enumValues = Enum.GetValues(targetType);
            foreach (var enumValue in enumValues)
            {
                var field = enumValue.GetType().GetField(enumValue.ToString());
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null && attribute.Description == valueString)
                {
                    return enumValue;
                }
            }

            return value;
        }
    }
}
