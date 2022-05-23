using System;
using System.Globalization;
using System.Windows.Data;

namespace accounting.ViewModels.Converter
{
    public class CurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "newCredit";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse(value.ToString(), NumberStyles.Currency);
        }
    }
}