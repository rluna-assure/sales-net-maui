using FieldSalesForce.Helpers;
using FieldSalesForce.ViewModels;

namespace FieldSalesForce.Views;

public partial class CartPage : ContentPage
{
    public CartPage(CartViewModel? viewModel = null)
    {
        InitializeComponent();
        BindingContext = viewModel ?? ServiceLocator.GetService<CartViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CartViewModel viewModel)
        {
            _ = viewModel.LoadCartAsync();
        }
    }
}
