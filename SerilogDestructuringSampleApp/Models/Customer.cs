using SerilogDestructuringSampleApp.Interfaces;

namespace SerilogDestructuringSampleApp.Models;

/// <summary>
/// Represents a customer with basic details such as personal and contact information.
/// </summary>
/// <remarks>
/// This class implements the <see cref="SerilogDestructuringSampleApp.Interfaces.ICustomer"/> interface 
/// and provides properties for storing customer-related data, including their work title, name, 
/// date of birth, office email, and phone number.
/// </remarks>
public class Customer : ICustomer
{
    public int Id { get; set; }
    public string WorkTitle { get; set; }
    public string FirstName { get; set; } 
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string OfficeEmail { get; set; }
    public string OfficePhoneNumber { get; set; }
}