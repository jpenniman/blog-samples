namespace CustomerManagement.Search.Repositories.EntityFramework;

class Address
{
    public long Id { get; set; }
    
    public long CustomerId { get; set; }

    public string PostalCode { get; set; } = string.Empty;
}