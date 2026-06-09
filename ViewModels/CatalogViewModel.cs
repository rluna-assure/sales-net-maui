using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FieldSalesForce.Models;
using FieldSalesForce.Services;
using FieldSalesForce.Views;
using Microsoft.Maui.Controls;

namespace FieldSalesForce.ViewModels;

public partial class CatalogViewModel : BaseViewModel
{
    private readonly ILocalDatabaseService _localDatabaseService;
    private List<Product> _allProducts = new();

    [ObservableProperty]
    private List<Product> _products = new();

    [ObservableProperty]
    private List<string> _categories = new();

    [ObservableProperty]
    private string _searchText = string.Empty;

    [ObservableProperty]
    private string _selectedCategory = "Todos";

    [ObservableProperty]
    private Product? _selectedProduct;

    public CatalogViewModel(ILocalDatabaseService localDatabaseService)
    {
        _localDatabaseService = localDatabaseService;
        Title = "Catálogo";
    }

    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            _allProducts = await _localDatabaseService.GetProductsAsync();
            Categories = new List<string> { "Todos" };
            Categories.AddRange(_allProducts.Select(x => x.Category).Distinct().OrderBy(x => x));
            SelectedCategory = "Todos";
            FilterProducts();
        }
        finally
        {
            IsBusy = false;
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterProducts();
    }

    partial void OnSelectedCategoryChanged(string value)
    {
        FilterProducts();
    }

    partial void OnSelectedProductChanged(Product? value)
    {
        if (value is null)
        {
            return;
        }

        _ = SelectProductAsync(value);
    }

    private void FilterProducts()
    {
        var query = SearchText?.Trim().ToLowerInvariant() ?? string.Empty;
        var filtered = _allProducts.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(query))
        {
            filtered = filtered.Where(x => x.Name.ToLowerInvariant().Contains(query) || x.Description.ToLowerInvariant().Contains(query) || x.Category.ToLowerInvariant().Contains(query));
        }

        if (!string.IsNullOrWhiteSpace(SelectedCategory) && SelectedCategory != "Todos")
        {
            filtered = filtered.Where(x => x.Category == SelectedCategory);
        }

        Products = filtered.ToList();
    }

    [RelayCommand]
    public async Task SelectProductAsync(Product product)
    {
        if (product is null)
        {
            return;
        }

        SelectedProduct = null;
        await Shell.Current.GoToAsync($"{nameof(ProductDetailPage)}?productId={product.Id}");
    }
}
