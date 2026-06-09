using System.Net.Http.Json;
using System.Text.Json;
using FieldSalesForce.Models;

namespace FieldSalesForce.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/products", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return new List<Product>();
            }

            var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
            var products = await JsonSerializer.DeserializeAsync<List<Product>>(stream, cancellationToken: cancellationToken);
            return products ?? new List<Product>();
        }
        catch (HttpRequestException)
        {
            return new List<Product>();
        }
        catch (TaskCanceledException)
        {
            return new List<Product>();
        }
    }

    public async Task<bool> SubmitOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("/api/orders", order, cancellationToken);
            return response.IsSuccessStatusCode;
        }
        catch (HttpRequestException)
        {
            return false;
        }
        catch (TaskCanceledException)
        {
            return false;
        }
    }
}
