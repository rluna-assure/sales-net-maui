using Microsoft.Maui.Networking;

namespace FieldSalesForce.Services;

public class ConnectivityService : IConnectivityService
{
    public bool IsConnected => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

    public event EventHandler<bool>? ConnectivityChanged;

    public Task InitializeAsync()
    {
        Connectivity.ConnectivityChanged += OnConnectivityChanged;
        return Task.CompletedTask;
    }

    private void OnConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
    {
        ConnectivityChanged?.Invoke(this, e.NetworkAccess == NetworkAccess.Internet);
    }
}
