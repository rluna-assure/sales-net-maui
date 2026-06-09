using System.Globalization;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.Helpers;

public class IntToDoubleConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is int intValue ? (double)intValue : 1.0;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is double doubleValue ? (int)doubleValue : 1;
    }
}
