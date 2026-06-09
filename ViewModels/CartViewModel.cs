using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FieldSalesForce.Models;
using FieldSalesForce.Services;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.ViewModels;

public partial class CartViewModel : BaseViewModel
{
    private readonly ILocalDatabaseService _localDatabaseService;
    private readonly ISyncService _syncService;

    [ObservableProperty]
    private List<CartItem> _cartItems = new();

    [ObservableProperty]
    private decimal _orderTotal;

    [ObservableProperty]
    private string _statusMessage = string.Empty;

    public CartViewModel(
        ILocalDatabaseService localDatabaseService,
        ISyncService syncService)
    {
        _localDatabaseService = localDatabaseService;
        _syncService = syncService;
        Title = "Carrito";
    }

    [RelayCommand]
    public async Task LoadCartAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            CartItems = await _localDatabaseService.GetCartItemsAsync();
            UpdateCartTotal();
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task CheckoutAsync()
    {
        if (IsBusy) return;
        if (!CartItems.Any())
        {
            ErrorMessage = "El carrito está vacío.";
            return;
        }

        try
        {
            IsBusy = true;
            ErrorMessage = string.Empty;

            var clients = await _localDatabaseService.GetClientsAsync();
            var client = clients.FirstOrDefault() ?? new Client
            {
                Name = "Cliente Offline",
                Email = "offline@cliente.local",
                Phone = "0000000000",
                Address = "Operación offline"
            };

            if (client.Id == 0)
            {
                await _localDatabaseService.SaveClientAsync(client);
            }

            var order = new Order
            {
                ClientId = client.Id,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Submitted,
                IsPendingSync = true,
                TotalAmount = OrderTotal
            };

            await _localDatabaseService.SaveOrderAsync(order);

            var orderItems = CartItems.Select(cartItem => new OrderItem
            {
                OrderId = order.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.ProductName,
                UnitPrice = cartItem.UnitPrice,
                Quantity = cartItem.Quantity
            }).ToList();

            await _localDatabaseService.SaveOrderItemsAsync(orderItems);
            await _localDatabaseService.ClearCartAsync();
            await LoadCartAsync();
            await _syncService.SyncPendingOrdersAsync();

            StatusMessage = "Pedido creado y sincronización en segundo plano iniciada.";
        }
        catch
        {
            ErrorMessage = "Error al procesar el pedido.";
        }
        finally
        {
            IsBusy = false;
            UpdateCartTotal();
        }
    }

    [RelayCommand]
    public async Task IncrementQuantityAsync(CartItem item)
    {
        if (item is null) return;
        item.Quantity++;
        await _localDatabaseService.AddOrUpdateCartItemAsync(item);
        await LoadCartAsync();
    }

    [RelayCommand]
    public async Task DecrementQuantityAsync(CartItem item)
    {
        if (item is null) return;
        if (item.Quantity <= 1)
        {
            await RemoveCartItemAsync(item);
            return;
        }

        item.Quantity--;
        await _localDatabaseService.AddOrUpdateCartItemAsync(item);
        await LoadCartAsync();
    }

    [RelayCommand]
    public async Task RemoveCartItemAsync(CartItem item)
    {
        if (item is null) return;
        await _localDatabaseService.RemoveCartItemAsync(item.Id);
        await LoadCartAsync();
    }

    private void UpdateCartTotal()
    {
        OrderTotal = CartItems.Sum(x => x.Subtotal);
    }
}
