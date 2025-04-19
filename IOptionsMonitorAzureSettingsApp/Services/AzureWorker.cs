using IOptionsMonitorAzureSettingsApp.Models;
using System.Text.Json;

namespace IOptionsMonitorAzureSettingsApp.Services;

/// <summary>
/// Represents a background service that monitors changes in Azure settings and performs related operations.
/// </summary>
/// <remarks>
/// The <see cref="AzureWorker"/> class extends <see cref="BackgroundService"/> to provide a long-running service
/// that listens for changes in Azure settings via the <see cref="SettingsMonitorService"/>. It ensures that
/// the application responds dynamically to configuration updates.
/// </remarks>
public class AzureWorker : BackgroundService
{
    private readonly SettingsMonitorService _settingsMonitor;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureWorker"/> class.
    /// </summary>
    /// <param name="settingsMonitor">
    /// An instance of <see cref="SettingsMonitorService"/> used to monitor changes in Azure settings.
    /// </param>
    /// <remarks>
    /// The constructor subscribes to the <see cref="SettingsMonitorService.SettingsChanged"/> event to handle
    /// configuration updates dynamically. This ensures that the application reacts to changes in Azure settings
    /// during runtime.
    /// </remarks>
    public AzureWorker(SettingsMonitorService settingsMonitor)
    {
        _settingsMonitor = settingsMonitor;
        _settingsMonitor.SettingsChanged += OnSettingsChanged;
    }

    /// <summary>
    /// Handles the event triggered when the Azure settings are updated.
    /// </summary>
    /// <param name="newSettings">
    /// The updated <see cref="AzureSettings1"/> instance containing the new configuration values.
    /// </param>
    /// <remarks>
    /// This method is invoked whenever the configuration settings for Azure are changed. 
    /// It logs the updated settings to the console in a serialized JSON format.
    /// </remarks>
    private void OnSettingsChanged(AzureSettings1 newSettings)
    {
        // This will be called when config changes
        Console.WriteLine("Settings updated:");
        Console.WriteLine(JsonSerializer.Serialize(newSettings, SettingsMonitorService.Options));
    }

    /// <summary>
    /// Executes the background service operation.
    /// </summary>
    /// <param name="stoppingToken">
    /// A <see cref="CancellationToken"/> that is triggered when the host is shutting down.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the background service execution.
    /// </returns>
    /// <remarks>
    /// This method is the entry point for the background service. It is invoked when the service starts
    /// and runs until the service is stopped. Override this method to implement the service's background
    /// processing logic.
    /// </remarks>
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Keep the service alive, run background tasks here if desired
        return Task.CompletedTask;
    }
}


