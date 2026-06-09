using System.Globalization;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.Helpers;

public class StringToBoolConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return !string.IsNullOrWhiteSpace(value as string);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is bool b && b ? "True" : string.Empty;
    }
}
