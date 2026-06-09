using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FieldSalesForce.Models;
using FieldSalesForce.Services;

namespace FieldSalesForce.ViewModels;

public partial class OrderHistoryViewModel : BaseViewModel
{
    private readonly ILocalDatabaseService _localDatabaseService;

    [ObservableProperty]
    private List<Order> _orders = new();

    public OrderHistoryViewModel(ILocalDatabaseService localDatabaseService)
    {
        _localDatabaseService = localDatabaseService;
        Title = "Historial";
    }

    [RelayCommand]
    public async Task LoadOrdersAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Orders = await _localDatabaseService.GetOrdersAsync();
        }
        finally
        {
            IsBusy = false;
        }
    }
}
