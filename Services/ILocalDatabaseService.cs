using FieldSalesForce.Models;

namespace FieldSalesForce.Services;

public interface ILocalDatabaseService
{
    Task InitializeAsync();
    Task<List<Product>> GetProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<int> SaveProductAsync(Product product);
    Task<int> SaveProductsAsync(IEnumerable<Product> products);

    Task<List<CartItem>> GetCartItemsAsync();
    Task<CartItem?> GetCartItemByProductIdAsync(int productId);
    Task<int> AddOrUpdateCartItemAsync(CartItem cartItem);
    Task<int> RemoveCartItemAsync(int cartItemId);
    Task<int> ClearCartAsync();

    Task<Client?> GetClientByIdAsync(int clientId);
    Task<int> SaveClientAsync(Client client);
    Task<List<Client>> GetClientsAsync();

    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<int> SaveOrderAsync(Order order);
    Task<int> SaveOrderItemsAsync(IEnumerable<OrderItem> orderItems);
    Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
    Task<List<Order>> GetOrdersAsync();
    Task<List<Order>> GetPendingOrdersAsync();
}
