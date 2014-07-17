using System;
using System.Globalization;
using System.Windows.Data;
using CodeConcussion.KVL.Entity;

namespace CodeConcussion.KVL.Converters
{
    public sealed class OperationToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var operation = value as Operation?;
            if (operation == null) return "";
            if (operation == Operation.Addition) return "+";
            if (operation == Operation.Multiplication) return "x";
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var display = value as string;
            if (display == "x") return Operation.Multiplication;
            return Operation.Addition;
        }
    }
}
