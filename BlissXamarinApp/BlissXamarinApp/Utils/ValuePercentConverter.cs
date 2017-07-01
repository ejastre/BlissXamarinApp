using System;
using System.Globalization;
using Xamarin.Forms;

namespace BlissXamarinApp.Utils
{
    class ValuePercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value / 10;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}