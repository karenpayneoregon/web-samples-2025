using IOptionsMonitorAzureSettingsApp.Models;
using Microsoft.Extensions.Options;

namespace IOptionsMonitorAzureSettingsApp.Services;

/// <summary>
/// 
/// </summary>
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

    public AzureSettings GetCurrent() => _current;

    public string GetSnapshotHash() => _lastSnapshot;

    private string ComputeSnapshot(AzureSettings settings) => $"{settings.ConnectionString}|{settings.TenantId}";
}
