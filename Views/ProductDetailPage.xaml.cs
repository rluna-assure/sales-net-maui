using FieldSalesForce.Helpers;
using FieldSalesForce.ViewModels;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.Views;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetailPage : ContentPage
{
    private int _productId;

    public ProductDetailPage(ProductDetailViewModel? viewModel = null)
    {
        InitializeComponent();
        BindingContext = viewModel ?? ServiceLocator.GetService<ProductDetailViewModel>();
    }

    public int ProductId
    {
        get => _productId;
        set
        {
            _productId = value;
            if (BindingContext is ProductDetailViewModel viewModel)
            {
                _ = viewModel.LoadProductAsync(value);
            }
        }
    }
}
