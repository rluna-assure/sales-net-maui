using FieldSalesForce.Views;

namespace FieldSalesForce;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        RegisterRoutes();
        BuildShellByPlatform();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(CatalogPage), typeof(CatalogPage));
        Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
        Routing.RegisterRoute(nameof(OrderHistoryPage), typeof(OrderHistoryPage));
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
    }

    private void BuildShellByPlatform()
    {
        Items.Clear();

        if (DeviceInfo.Idiom == DeviceIdiom.Phone || DeviceInfo.Idiom == DeviceIdiom.Tablet)
        {
            BuildMobileShell();
            return;
        }

        BuildDesktopShell();
    }

    private void BuildMobileShell()
    {
        var tabBar = new TabBar
        {
            Items =
            {
                CreateTab("Catálogo", typeof(CatalogPage)),
                CreateTab("Carrito", typeof(CartPage)),
                CreateTab("Historial", typeof(OrderHistoryPage))
            }
        };

        Items.Add(tabBar);
    }

    private void BuildDesktopShell()
    {
        Items.Add(CreateFlyoutItem("Catálogo", typeof(CatalogPage)));
        Items.Add(CreateFlyoutItem("Carrito", typeof(CartPage)));
        Items.Add(CreateFlyoutItem("Historial", typeof(OrderHistoryPage)));
    }

    private static Tab CreateTab(string title, Type pageType)
    {
        return new Tab
        {
            Title = title,
            Items =
            {
                new ShellContent
                {
                    Title = title,
                    ContentTemplate = new DataTemplate(pageType)
                }
            }
        };
    }

    private static FlyoutItem CreateFlyoutItem(string title, Type pageType)
    {
        return new FlyoutItem
        {
            Title = title,
            Items =
            {
                new ShellContent
                {
                    Title = title,
                    ContentTemplate = new DataTemplate(pageType)
                }
            }
        };
    }
}
