namespace IOptionsMonitorAzureSettingsApp.Models;

/// <summary>
/// For Page Index1.cshtml
/// 
/// Represents the configuration settings for Azure, including connection details and tenant information.
/// </summary>
/// <remarks>
/// This class is used to store and manage Azure-related configuration settings, such as the connection string
/// and tenant ID. It is designed to be used in conjunction with <see cref="Microsoft.Extensions.Options.IOptionsMonitor{TOptions}"/>
/// to enable dynamic updates to the configuration at runtime.
/// </remarks>
public class AzureSettings1
{
    public string ConnectionString { get; set; } = string.Empty;
    public string TenantId { get; set; } = string.Empty;
}