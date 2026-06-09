using FieldSalesForce.Helpers;
using FieldSalesForce.ViewModels;

namespace FieldSalesForce.Views;

public partial class OrderHistoryPage : ContentPage
{
    public OrderHistoryPage(OrderHistoryViewModel? viewModel = null)
    {
        InitializeComponent();
        BindingContext = viewModel ?? ServiceLocator.GetService<OrderHistoryViewModel>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is OrderHistoryViewModel viewModel)
        {
            _ = viewModel.LoadOrdersAsync();
        }
    }
}
