using IOptionsMonitorAzureSettingsApp.Models;
using IOptionsMonitorAzureSettingsApp.Services;
using Serilog;

namespace IOptionsMonitorAzureSettingsApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Information()
            .WriteTo.Console()
            .CreateLogger();

        builder.Host.UseSerilog();


        // Load configuration with reload on change
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // Register IOptionsMonitor with reloading enabled for Index page
        builder.Services.Configure<AzureSettings>(builder.Configuration.GetSection("AzureSettings"));
        builder.Services.Configure<AzureSettings>("TenantName", builder.Configuration.GetSection("TenantNameAzureSettings"));


        // Service to monitor changes in Azure settings in Index1 page
        builder.Services.Configure<AzureSettings1>(builder.Configuration.GetSection("Azure"));
        builder.Services.AddSingleton<SettingsMonitorService>();
        builder.Services.AddHostedService<AzureWorker>();

        // Add services to the container.
        builder.Services.AddRazorPages();



        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapRazorPages()
           .WithStaticAssets();

        app.Run();
    }
}
