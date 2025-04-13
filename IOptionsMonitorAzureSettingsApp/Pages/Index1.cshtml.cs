using IOptionsMonitorAzureSettingsApp.Models;
using IOptionsMonitorAzureSettingsApp.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
#pragma warning disable CS8618, CS9264

namespace IOptionsMonitorAzureSettingsApp.Pages
{
    public class Index1Model : PageModel
    {
        private readonly AzureService _azureService;
        private static string? _latestChangeMessage;

        public AzureSettings DefaultSettings { get; private set; }
        public AzureSettings NamedSettings { get; private set; }
        public string? ChangeMessage => _latestChangeMessage;

        public Index1Model(AzureService azureService)
        {
            _azureService = azureService;

            // Subscribe only once per app domain
            _azureService.OnSettingsChanged += msg => _latestChangeMessage = msg;
        }

        public void OnGet()
        {
            DefaultSettings = _azureService.GetDefaultSettings();
            NamedSettings = _azureService.GetNamedSettings();
        }
    }
}
