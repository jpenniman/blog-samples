namespace Northwind.CustomerManagement.Api.DataMaintenance;

/// <summary>
/// Represents a customer in the system.
/// </summary>
public sealed class Customer
{
    Customer() { }

    /// <summary>
    /// Creates a new customer.
    /// </summary>
    /// <param name="companyName">The company name.</param>
    /// <returns>A new instance of a customer with the given company name.</returns>
    public static Customer Create(string companyName)
    {
        return new Customer() { CompanyName = companyName };
    }

    /// <summary>
    /// Unique Id of the customer.
    /// </summary>
    public long Id { get; internal set; }

    /// <summary>
    /// The Company name.
    /// </summary>
    public string CompanyName { get; private set; } = string.Empty;

    /// <summary>
    /// Optional phone number.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Version
    /// </summary>
    public long Version { get; set; }
}
