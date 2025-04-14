using IOptionsMonitorAzureSettingsApp.Models;
using IOptionsMonitorAzureSettingsApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
#pragma warning disable CS8618, CS9264

namespace IOptionsMonitorAzureSettingsApp.Pages
{
    public class Index1Model : PageModel
    {
        private readonly SettingsMonitorService _monitor;

        public AzureSettings Settings { get; private set; }
        public string SnapshotHash { get; private set; }

        public Index1Model(SettingsMonitorService monitor)
        {
            _monitor = monitor;
        }

        public void OnGet()
        {
            // ✅ Get the latest settings directly from singleton service
            Settings = _monitor.GetCurrent();
            SnapshotHash = _monitor.GetSnapshotHash();
            Console.WriteLine($"[Page Rendered] Settings: {SnapshotHash}");
        }
    }
}

