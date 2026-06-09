using FieldSalesForce.Helpers;
using FieldSalesForce.ViewModels;

namespace FieldSalesForce.Views;

public partial class CatalogPage : ContentPage
{
    public CatalogPage(CatalogViewModel? viewModel = null)
    {
        InitializeComponent();
        BindingContext = viewModel ?? ServiceLocator.GetService<CatalogViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CatalogViewModel viewModel)
        {
            _ = viewModel.LoadProductsAsync();
        }
    }
}
