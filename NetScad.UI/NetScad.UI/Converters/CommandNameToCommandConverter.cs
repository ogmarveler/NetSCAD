using Avalonia.Data.Converters;
using NetScad.UI.ViewModels;
using System;
using System.Globalization;
using System.Windows.Input;

namespace NetScad.UI.Converters
{
    public class CommandNameToCommandConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string commandName && parameter is MainWindowViewModel viewModel)
            {
                var property = viewModel.GetType().GetProperty(commandName);
                return property?.GetValue(viewModel) as ICommand;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
