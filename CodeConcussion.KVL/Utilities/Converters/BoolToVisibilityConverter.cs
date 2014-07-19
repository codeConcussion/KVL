using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CodeConcussion.KVL.Utilities.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as bool? ?? false) ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Visibility? ?? Visibility.Hidden) == Visibility.Visible;
        }
    }
}
