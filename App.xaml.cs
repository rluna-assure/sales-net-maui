using FieldSalesForce.Services;

namespace FieldSalesForce;

public partial class App : Application
{
    private readonly ILocalDatabaseService _localDatabaseService;
    private readonly ISyncService _syncService;

    public App(
        ILocalDatabaseService localDatabaseService,
        ISyncService syncService)
    {
        InitializeComponent();

        _localDatabaseService = localDatabaseService;
        _syncService = syncService;

        MainPage = new AppShell();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await _localDatabaseService.InitializeAsync();
        await _syncService.StartAsync();
    }
}
