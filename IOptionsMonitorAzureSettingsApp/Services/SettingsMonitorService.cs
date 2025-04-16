using System.Text.Json;
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
    private AzureSettings1 _current;
    private string _lastSnapshot;

    public event Action<AzureSettings1>? SettingsChanged;

    public SettingsMonitorService(IOptionsMonitor<AzureSettings1> monitor)
    {
        _current = monitor.CurrentValue;
        _lastSnapshot = ComputeSnapshot(_current);

        monitor.OnChange(updated =>
        {
            _current = updated;
            _lastSnapshot = ComputeSnapshot(updated);

            // Notify subscribers
            SettingsChanged?.Invoke(_current);
        });
    }

    public AzureSettings1 GetCurrent() => _current;
    public string GetSnapshotHash() => _lastSnapshot;

    private string ComputeSnapshot(AzureSettings1 settings) =>
        JsonSerializer.Serialize(settings, Options);

    public static JsonSerializerOptions Options => new() { WriteIndented = true };
}
