namespace SerilogDestructuringSampleApp.Models;

/// <summary>
/// Represents a gold-tier customer with additional benefits and properties.
/// </summary>
/// <remarks>
/// This class extends the <see cref="Customer"/> class by adding properties specific to gold-tier customers, 
/// such as a gold card number and its expiry date.
/// </remarks>
public class GoldCustomer : Customer
{
    public string GoldCardNumber { get; set; }
    public DateTime GoldCardExpiryDate { get; set; }
}