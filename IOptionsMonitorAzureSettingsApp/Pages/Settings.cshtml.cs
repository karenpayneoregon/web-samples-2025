using IOptionsMonitorAzureSettingsApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace IOptionsMonitorAzureSettingsApp.Pages;

/// <summary>
/// Represents the Razor Page model for handling settings-related operations which is part of the refresh for
/// Index1 page JavaScript fetch request.
/// </summary>
/// <remarks>
/// This class is responsible for managing the retrieval of settings data and exposing it
/// through the Razor Page. It utilizes the <see cref="SettingsMonitorService"/> to monitor
/// and fetch the current settings snapshot.
/// </remarks>
public class SettingsModel : PageModel
{
    private readonly SettingsMonitorService _monitor;

    public SettingsModel(SettingsMonitorService monitor)
    {
        _monitor = monitor;
    }

    public IActionResult OnGet()
    {
        return new JsonResult(new
        {
            snapshotHash = _monitor.GetSnapshotHash()
        });
    }
}