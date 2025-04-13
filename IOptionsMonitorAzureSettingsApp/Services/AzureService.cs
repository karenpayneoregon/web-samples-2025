using IOptionsMonitorAzureSettingsApp.Models;


namespace IOptionsMonitorAzureSettingsApp.Services;

using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

/// <summary>
/// Provides a service for monitoring and handling changes to Azure settings using <see cref="IOptionsMonitor{TOptions}"/>.
/// </summary>
/// <remarks>
/// This service subscribes to changes in Azure settings and notifies subscribers when updates occur.
/// It is designed to work with the <see cref="AzureSettings"/> model and supports real-time configuration updates.
/// </remarks>
public class AzureService
{
    private readonly IOptionsMonitor<AzureSettings> _optionsMonitor;
    private readonly ConcurrentDictionary<string, string> _lastKnownValues = new();

    public event Action<string>? OnSettingsChanged;

    public AzureService(IOptionsMonitor<AzureSettings> optionsMonitor)
    {
        _optionsMonitor = optionsMonitor;

        // Load initial values
        StoreSettings(_optionsMonitor.CurrentValue, "Default");
        StoreSettings(_optionsMonitor.Get("TenantName"), "TenantName");

        // Subscribe to changes
        _optionsMonitor.OnChange((updatedSettings, name) =>
        {
            if (string.IsNullOrEmpty(name)) // Default settings changed
            {
                CheckForChanges(updatedSettings, "Default");
            }
            else if (name == "TenantName") // Named settings changed
            {
                CheckForChanges(_optionsMonitor.Get("TenantName"), "TenantName");
            }
        });
    }

    /// <summary>
    /// Checks for changes in the provided Azure settings and determines if any updates have occurred.
    /// </summary>
    /// <param name="newSettings">The updated <see cref="AzureSettings"/> instance to compare against the last known values.</param>
    /// <param name="key">The key associated with the settings being checked (e.g., "Default" or "TenantName").</param>
    /// <remarks>
    /// If changes are detected in the <see cref="AzureSettings.ConnectionString"/> or <see cref="AzureSettings.TenantId"/>,
    /// the method updates the stored settings and triggers the <see cref="OnSettingsChanged"/> event with a descriptive message.
    /// </remarks>
    private void CheckForChanges(AzureSettings newSettings, string key)
    {
        bool hasChanged = false;
        string message = $"{key} settings changed: ";

        var test = $"{key}_ConnectionString";

        if (_lastKnownValues.TryGetValue($"{key}_ConnectionString", out var oldConnectionString) && oldConnectionString != newSettings.ConnectionString)
        {
            message += "ConnectionString updated. ";
            hasChanged = true;
        }

        if (_lastKnownValues.TryGetValue($"{key}_TenantId", out var oldTenantId) && oldTenantId != newSettings.TenantId)
        {
            message += "TenantId updated.";
            hasChanged = true;
        }

        if (hasChanged)
        {
            StoreSettings(newSettings, key);
            OnSettingsChanged?.Invoke(message);
        }
    }

    private void StoreSettings(AzureSettings settings, string key)
    {
        _lastKnownValues[$"{key}_ConnectionString"] = settings.ConnectionString;
        _lastKnownValues[$"{key}_TenantId"] = settings.TenantId;
    }

    public AzureSettings GetDefaultSettings() => _optionsMonitor.CurrentValue;
    public AzureSettings GetNamedSettings() => _optionsMonitor.Get("TenantName");

}