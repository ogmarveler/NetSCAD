using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace NetScad.UI.Converters
{
    public class DoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // VM -> View: Convert double to string
            return value is double d ? d.ToString(CultureInfo.InvariantCulture) : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // View -> VM: Convert string to double
            if (value is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return 0.0; // Default for empty input
                }
                if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
                {
                    return result;
                }
            }
            return 0.0; // Fallback for invalid input
        }
    }
}
