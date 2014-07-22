using System.Windows;

namespace CodeConcussion.KVL.Utilities.Xaml
{
    public class BoolToVisibilityConverter : BoolToValueConverter<Visibility>
    {
        public BoolToVisibilityConverter()
        {

        }

        public BoolToVisibilityConverter(Visibility trueValue, Visibility falseValue)
        {
            TrueValue = trueValue;
            FalseValue = falseValue;
        }
    }
}