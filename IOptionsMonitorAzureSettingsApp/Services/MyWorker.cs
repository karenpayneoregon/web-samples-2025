using IOptionsMonitorAzureSettingsApp.Models;
using System.Text.Json;

namespace IOptionsMonitorAzureSettingsApp.Services;

public class MyWorker : BackgroundService
{
    private readonly SettingsMonitorService _settingsMonitor;

    public MyWorker(SettingsMonitorService settingsMonitor)
    {
        _settingsMonitor = settingsMonitor;
        _settingsMonitor.SettingsChanged += OnSettingsChanged;
    }

    private void OnSettingsChanged(AzureSettings1 newSettings)
    {
        // This will be called when config changes
        Console.WriteLine("Settings updated:");
        Console.WriteLine(JsonSerializer.Serialize(newSettings, SettingsMonitorService.Options));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Keep the service alive, maybe run background tasks here
        return Task.CompletedTask;
    }
}


