using IOptionsMonitorAzureSettingsApp.Models;
using Microsoft.Extensions.Options;

namespace IOptionsMonitorAzureSettingsApp.Services;

/// <summary>
/// Provides functionality to monitor and manage changes to <see cref="AzureSettings"/> using <see cref="IOptionsMonitor{TOptions}"/>.
/// </summary>
/// <remarks>
/// This service listens for configuration changes and updates the current settings and a snapshot hash accordingly.
/// It is designed to be used as a singleton service for monitoring Azure-related settings in the application.
/// </remarks>
public class SettingsMonitorService
{
    private AzureSettings _current;
    private string _lastSnapshot;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsMonitorService"/> class.
    /// </summary>
    /// <param name="monitor">
    /// An instance of <see cref="IOptionsMonitor{TOptions}"/> for monitoring changes to <see cref="AzureSettings"/>.
    /// </param>
    /// <remarks>
    /// This constructor sets up a change listener to update the current settings and snapshot hash
    /// whenever the configuration changes.
    /// </remarks>
    public SettingsMonitorService(IOptionsMonitor<AzureSettings> monitor)
    {
        _current = monitor.CurrentValue;
        _lastSnapshot = ComputeSnapshot(_current);

        monitor.OnChange(updated =>
        {
            _current = updated;
            _lastSnapshot = ComputeSnapshot(updated);
            Console.WriteLine($"[Config Changed] New: {_lastSnapshot}");
        });
    }

    /// <summary>
    /// Retrieves the current instance of <see cref="AzureSettings"/> being monitored.
    /// </summary>
    /// <returns>
    /// The current <see cref="AzureSettings"/> object representing the latest configuration values.
    /// </returns>
    /// <remarks>
    /// This method provides access to the most recent configuration settings for Azure-related operations.
    /// It is updated automatically when changes are detected in the configuration source.
    /// </remarks>
    public AzureSettings GetCurrent() => _current;

    /// <summary>
    /// Retrieves the hash of the current snapshot of <see cref="AzureSettings"/>.
    /// </summary>
    /// <returns>
    /// A <see cref="string"/> representing the hash of the latest snapshot of the monitored settings.
    /// </returns>
    /// <remarks>
    /// This method provides a unique identifier for the current configuration snapshot, 
    /// which can be used to detect changes or verify consistency in the monitored settings.
    /// </remarks>
    public string GetSnapshotHash() => _lastSnapshot;

    /// <summary>
    /// Computes a unique snapshot hash for the provided <see cref="AzureSettings"/> instance.
    /// </summary>
    /// <param name="settings">
    /// The <see cref="AzureSettings"/> instance for which the snapshot hash is to be computed.
    /// </param>
    /// <returns>
    /// A <see cref="string"/> representing the computed hash of the provided <see cref="AzureSettings"/> instance.
    /// </returns>
    /// <remarks>
    /// This method concatenates key properties of the <see cref="AzureSettings"/> object to generate a unique identifier
    /// for the current configuration. The resulting hash can be used to track changes or ensure consistency.
    /// </remarks>
    private string ComputeSnapshot(AzureSettings settings) => $"{settings.ConnectionString}|{settings.TenantId}";
}
