using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.Helpers;

public static class ServiceLocator
{
    public static IServiceProvider? Services => Application.Current?.Handler?.MauiContext?.Services;

    public static T? GetService<T>() where T : class
    {
        return Services?.GetService<T>();
    }
}
