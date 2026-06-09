namespace FieldSalesForce.Services;

public interface ISyncService
{
    Task StartAsync(CancellationToken cancellationToken = default);
    Task SyncPendingOrdersAsync(CancellationToken cancellationToken = default);
}
