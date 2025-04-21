using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Serilog;
using Serilog.Core;
using SerilogDestructuringSampleApp.Classes;
using SerilogDestructuringSampleApp.Models;
using Spectre.Console;
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
// ReSharper disable ConvertConstructorToMemberInitializers

namespace SerilogDestructuringSampleApp.Pages;
public class IndexModel : PageModel
{
    [BindProperty]
    public List<Customer> Customers { get; set; }
    public IndexModel()
    {
        Customers = MockedData.Customers();
    }
    /// <summary>
    /// Demonstrates the use of Serilog for <see cref="IDestructuringPolicy"/> logging and Spectre.Console for console output.
    /// It logs customer details, including distinguishing gold-tier customers, and logs database connection details.
    /// </summary>
    public void OnGet()
    {
        AnsiConsole.MarkupLine("[deeppink1]Customers:[/]");

        foreach (var customer in Customers)
        {
            if (customer is GoldCustomer)
                Log.Information("Gold customer {@C}", customer);
            else
                Log.Information("Customer {@C}", customer);
        }

        Console.WriteLine();

        AnsiConsole.MarkupLine("[deeppink1]Connection:[/]");

        const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NorthWind2022;Integrated Security=True";
        var connection = new SqlConnection(connectionString);
        Log.Information("Connection {@C}", connection);
        
    }
}
