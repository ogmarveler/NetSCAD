using Avalonia.Data.Converters;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace NetScad.UI.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
                var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                   .FirstOrDefault() as DescriptionAttribute;
                return descriptionAttribute?.Description ?? enumValue.ToString();
            }
            return value?.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
