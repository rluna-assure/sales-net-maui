namespace FieldSalesForce.Services;

public class SyncService : ISyncService
{
    private readonly ILocalDatabaseService _localDatabaseService;
    private readonly IApiService _apiService;
    private readonly IConnectivityService _connectivityService;

    public SyncService(
        ILocalDatabaseService localDatabaseService,
        IApiService apiService,
        IConnectivityService connectivityService)
    {
        _localDatabaseService = localDatabaseService;
        _apiService = apiService;
        _connectivityService = connectivityService;
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _connectivityService.InitializeAsync();
        if (_connectivityService.IsConnected)
        {
            await SyncPendingOrdersAsync(cancellationToken);
        }

        _connectivityService.ConnectivityChanged += async (sender, isConnected) =>
        {
            if (isConnected)
            {
                await SyncPendingOrdersAsync(cancellationToken);
            }
        };
    }

    public async Task SyncPendingOrdersAsync(CancellationToken cancellationToken = default)
    {
        if (!_connectivityService.IsConnected)
        {
            return;
        }

        var pendingOrders = await _localDatabaseService.GetPendingOrdersAsync();
        foreach (var order in pendingOrders)
        {
            if (await _apiService.SubmitOrderAsync(order, cancellationToken))
            {
                order.IsPendingSync = false;
                order.Status = Models.OrderStatus.Synced;
                await _localDatabaseService.SaveOrderAsync(order);
            }
        }
    }
}
