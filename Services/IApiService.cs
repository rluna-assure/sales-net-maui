using FieldSalesForce.Models;

namespace FieldSalesForce.Services;

public interface IApiService
{
    Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default);
    Task<bool> SubmitOrderAsync(Order order, CancellationToken cancellationToken = default);
}
