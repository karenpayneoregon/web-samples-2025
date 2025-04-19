using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using IOptionsMonitorAzureSettingsApp.Models;
using Microsoft.Extensions.Options;

namespace IOptionsMonitorAzureSettingsApp.Services;

/// <summary>
/// Provides functionality to monitor changes to <see cref="AzureSettings1"/> and notify subscribers
/// when the settings are updated.
/// </summary>
/// <remarks>
/// This service leverages <see cref="IOptionsMonitor{TOptions}"/> to track changes to the configuration
/// of <see cref="AzureSettings1"/>. It computes a snapshot hash of the current settings and raises the
/// <see cref="SettingsChanged"/> event when a change is detected.
/// </remarks>
public class SettingsMonitorService
{
    private AzureSettings1 _current;
    private string _lastSnapshot;

    public event Action<AzureSettings1>? SettingsChanged;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsMonitorService"/> class.
    /// </summary>
    /// <param name="monitor">
    /// An <see cref="IOptionsMonitor{TOptions}"/> instance for monitoring changes to <see cref="AzureSettings1"/>.
    /// </param>
    /// <remarks>
    /// This constructor sets up a listener for changes to the monitored <see cref="AzureSettings1"/> instance.
    /// When changes are detected, it updates the current settings, computes a new snapshot hash, and triggers the
    /// <see cref="SettingsChanged"/> event if the snapshot has changed.
    /// </remarks>
    public SettingsMonitorService(IOptionsMonitor<AzureSettings1> monitor)
    {
        _current = monitor.CurrentValue;
        _lastSnapshot = ComputeSnapshot(_current);

        monitor.OnChange(updated =>
        {
            var newSnapshot = ComputeSnapshot(updated);

            // Only invoke if the snapshot really changed
            if (newSnapshot == _lastSnapshot) return;
            _current = updated;
            _lastSnapshot = newSnapshot;
            SettingsChanged?.Invoke(_current);
        });
    }

    public AzureSettings1 GetCurrent() => _current;
    public string GetSnapshotHash() => _lastSnapshot;

    /// <summary>
    /// Computes a unique hash representing the current state of the provided <see cref="AzureSettings1"/> instance.
    /// </summary>
    /// <param name="settings">The <see cref="AzureSettings1"/> instance to compute the snapshot hash for.</param>
    /// <returns>A base64-encoded string representing the SHA-256 hash of the serialized <see cref="AzureSettings1"/> instance.</returns>
    /// <remarks>
    /// This method serializes the provided <see cref="AzureSettings1"/> instance into JSON format using the specified
    /// <see cref="JsonSerializerOptions"/> and computes a SHA-256 hash of the serialized data. The resulting hash
    /// is then encoded as a base64 string for storage or comparison purposes.
    /// </remarks>
    private string ComputeSnapshot(AzureSettings1 settings)
    {
        var json = JsonSerializer.Serialize(settings, Options);
        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(hash);
    }

    public static JsonSerializerOptions Options => new() { WriteIndented = false };
}
