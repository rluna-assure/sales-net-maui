using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FieldSalesForce.Models;
using FieldSalesForce.Services;

namespace FieldSalesForce.ViewModels;

public partial class ProductDetailViewModel : BaseViewModel
{
    private readonly ILocalDatabaseService _localDatabaseService;

    [ObservableProperty]
    private Product? _product;

    [ObservableProperty]
    private int _quantity = 1;

    public ProductDetailViewModel(ILocalDatabaseService localDatabaseService)
    {
        _localDatabaseService = localDatabaseService;
        Title = "Detalle del producto";
    }

    public async Task LoadProductAsync(int productId)
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Product = await _localDatabaseService.GetProductByIdAsync(productId);
            Quantity = 1;
            Title = Product?.Name ?? "Detalle del producto";
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task AddToCartAsync()
    {
        if (Product is null)
        {
            ErrorMessage = "No se pudo cargar el producto.";
            return;
        }

        try
        {
            IsBusy = true;

            var cartItem = await _localDatabaseService.GetCartItemByProductIdAsync(Product.Id);
            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    ProductId = Product.Id,
                    ProductName = Product.Name,
                    UnitPrice = Product.Price,
                    Quantity = Quantity
                };

                await _localDatabaseService.AddOrUpdateCartItemAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += Quantity;
                await _localDatabaseService.AddOrUpdateCartItemAsync(cartItem);
            }

            ErrorMessage = string.Empty;
        }
        catch
        {
            ErrorMessage = "No se pudo agregar el producto al carrito.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}
