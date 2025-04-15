using IOptionsMonitorAzureSettingsApp.Models;
using IOptionsMonitorAzureSettingsApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
#pragma warning disable CS8618, CS9264

namespace IOptionsMonitorAzureSettingsApp.Pages;

/// <summary>
/// Represents the model for the Index1 Razor Page, providing functionality to retrieve and display
/// the latest Azure settings and their corresponding snapshot hash.
/// </summary>
public class Index1Model(SettingsMonitorService monitor) : PageModel
{
    public AzureSettings Settings { get; private set; }
    public string SnapshotHash { get; private set; }

    /// <summary>
    /// Handles GET requests for the page and initializes the latest Azure settings and snapshot hash.
    /// </summary>
    /// <remarks>
    /// This method retrieves the current <see cref="AzureSettings"/> and its corresponding snapshot hash
    /// from the <see cref="SettingsMonitorService"/> singleton service. It ensures that the page is rendered
    /// with the most up-to-date configuration values.
    /// </remarks>
    public void OnGet()
    {
        Settings = monitor.GetCurrent();
        SnapshotHash = monitor.GetSnapshotHash();
    }
}