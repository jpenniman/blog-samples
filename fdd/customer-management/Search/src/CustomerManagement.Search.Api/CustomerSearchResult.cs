using System.Runtime.Serialization;

namespace CustomerManagement.Search.Api;

/// <summary>
/// Represents a customer found while performing a search.
/// </summary>
[PublicAPI]
[DataContract]
public sealed class CustomerSearchResult
{
#pragma warning disable CS8618
    CustomerSearchResult() {}
#pragma warning restore CS8618

    internal CustomerSearchResult(long id, string companyName, string? phone = null, string? postalCode = null)
    {
        Id = id;
        CompanyName = companyName;
        Phone = phone;
        PostalCode = postalCode;
    }
    
    /// <summary>
    /// The Customer's ID
    /// </summary>
    [DataMember(Order = 1)]
    public long Id { get; private set; }

    /// <summary>
    /// The company name.
    /// </summary>
    [DataMember(Order = 2)]
    public string CompanyName { get; private set; }

    /// <summary>
    /// Optional phone number
    /// </summary>
    [DataMember(Order = 3)]
    public string? Phone { get; private set; }
    
    /// <summary>
    /// Optional postal code.
    /// </summary>
    [DataMember(Order = 4)]
    public string? PostalCode { get; private set; }
}