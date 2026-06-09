using FieldSalesForce.Services;
using FieldSalesForce.ViewModels;
using FieldSalesForce.Views;
using Microsoft.Extensions.Logging;

namespace FieldSalesForce;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri("https://api.example.com")
        });
        builder.Services.AddSingleton<IApiService, ApiService>();

        builder.Services.AddSingleton<ILocalDatabaseService, LocalDatabaseService>();
        builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();
        builder.Services.AddSingleton<ISyncService, SyncService>();

        builder.Services.AddSingleton<CatalogViewModel>();
        builder.Services.AddSingleton<CartViewModel>();
        builder.Services.AddSingleton<OrderHistoryViewModel>();
        builder.Services.AddSingleton<ProductDetailViewModel>();

        builder.Services.AddSingleton<CatalogPage>();
        builder.Services.AddSingleton<CartPage>();
        builder.Services.AddSingleton<OrderHistoryPage>();
        builder.Services.AddSingleton<ProductDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
