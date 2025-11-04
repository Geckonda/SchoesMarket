using Avalonia.Data.Converters;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoesMarket.Converters
{
    public class DiscountToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if(value is int discount)
            {
                return discount > 15 ? new SolidColorBrush(Color.Parse("#2E8B57")) : new SolidColorBrush(Colors.Red);
            }
            return new SolidColorBrush(Colors.Red);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
