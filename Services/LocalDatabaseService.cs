using FieldSalesForce.Models;
using SQLite;

namespace FieldSalesForce.Services;

public class LocalDatabaseService : ILocalDatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public LocalDatabaseService()
    {
        var databasePath = Path.Combine(FileSystem.AppDataDirectory, "fieldsalesforce.db3");
        _database = new SQLiteAsyncConnection(databasePath);
    }

    public async Task InitializeAsync()
    {
        await _database.CreateTableAsync<Product>();
        await _database.CreateTableAsync<Client>();
        await _database.CreateTableAsync<Order>();
        await _database.CreateTableAsync<OrderItem>();
        await _database.CreateTableAsync<CartItem>();

        await SeedProductsAsync();
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        try
        {
            return await _database.Table<Product>().Where(x => x.IsActive).ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<Product>();
        }
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        try
        {
            return await _database.FindAsync<Product>(id);
        }
        catch (SQLiteException)
        {
            return null;
        }
    }

    public async Task<int> SaveProductAsync(Product product)
    {
        try
        {
            return product.Id == 0
                ? await _database.InsertAsync(product)
                : await _database.UpdateAsync(product);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<int> SaveProductsAsync(IEnumerable<Product> products)
    {
        try
        {
            return await _database.InsertAllAsync(products);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<List<CartItem>> GetCartItemsAsync()
    {
        try
        {
            return await _database.Table<CartItem>().ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<CartItem>();
        }
    }

    public async Task<CartItem?> GetCartItemByProductIdAsync(int productId)
    {
        try
        {
            return await _database.Table<CartItem>().FirstOrDefaultAsync(x => x.ProductId == productId);
        }
        catch (SQLiteException)
        {
            return null;
        }
    }

    public async Task<int> AddOrUpdateCartItemAsync(CartItem cartItem)
    {
        try
        {
            if (cartItem.Id == 0)
            {
                return await _database.InsertAsync(cartItem);
            }

            return await _database.UpdateAsync(cartItem);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<int> RemoveCartItemAsync(int cartItemId)
    {
        try
        {
            return await _database.DeleteAsync<CartItem>(cartItemId);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<int> ClearCartAsync()
    {
        try
        {
            return await _database.DeleteAllAsync<CartItem>();
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<Client?> GetClientByIdAsync(int clientId)
    {
        try
        {
            return await _database.FindAsync<Client>(clientId);
        }
        catch (SQLiteException)
        {
            return null;
        }
    }

    public async Task<int> SaveClientAsync(Client client)
    {
        try
        {
            return client.Id == 0
                ? await _database.InsertAsync(client)
                : await _database.UpdateAsync(client);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<List<Client>> GetClientsAsync()
    {
        try
        {
            return await _database.Table<Client>().ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<Client>();
        }
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        try
        {
            return await _database.FindAsync<Order>(orderId);
        }
        catch (SQLiteException)
        {
            return null;
        }
    }

    public async Task<int> SaveOrderAsync(Order order)
    {
        try
        {
            return order.Id == 0
                ? await _database.InsertAsync(order)
                : await _database.UpdateAsync(order);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<int> SaveOrderItemsAsync(IEnumerable<OrderItem> orderItems)
    {
        try
        {
            return await _database.InsertAllAsync(orderItems);
        }
        catch (SQLiteException)
        {
            return 0;
        }
    }

    public async Task<List<OrderItem>> GetOrderItemsAsync(int orderId)
    {
        try
        {
            return await _database.Table<OrderItem>()
                .Where(x => x.OrderId == orderId)
                .ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<OrderItem>();
        }
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        try
        {
            return await _database.Table<Order>()
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<Order>();
        }
    }

    public async Task<List<Order>> GetPendingOrdersAsync()
    {
        try
        {
            return await _database.Table<Order>()
                .Where(x => x.IsPendingSync)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }
        catch (SQLiteException)
        {
            return new List<Order>();
        }
    }

    private async Task SeedProductsAsync()
    {
        try
        {
            var count = await _database.Table<Product>().CountAsync();
            if (count > 0)
            {
                return;
            }

            var seedProducts = new List<Product>
            {
                new() { Sku = "PRD-001", Name = "Café Premium", Category = "Bebidas", Description = "Café molido de exportación", Price = 12.50m, ImageUrl = string.Empty },
                new() { Sku = "PRD-002", Name = "Galletas Artesanales", Category = "Snacks", Description = "Galletas de mantequilla con chocolate", Price = 8.99m, ImageUrl = string.Empty },
                new() { Sku = "PRD-003", Name = "Refresco Natural", Category = "Bebidas", Description = "Refresco de frutas en envase retornable", Price = 5.75m, ImageUrl = string.Empty }
            };

            await _database.InsertAllAsync(seedProducts);
        }
        catch (SQLiteException)
        {
            // Seed failures are not fatal; continue with an empty catalog.
        }
    }
}
