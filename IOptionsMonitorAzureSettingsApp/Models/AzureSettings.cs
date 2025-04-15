using Microsoft.Extensions.Options;

namespace IOptionsMonitorAzureSettingsApp.Models;

/// <summary>
/// For Page Index.cshtml
/// 
/// Represents the configuration settings for Azure, including connection details and tenant information.
/// </summary>
/// <remarks>
/// This class is used to bind Azure-related configuration values from the application's configuration files.
/// It is registered with <see cref="IOptionsMonitor{TOptions}"/> to enable monitoring and reloading of settings
/// when changes occur in the configuration.
/// </remarks>
public class AzureSettings
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
}