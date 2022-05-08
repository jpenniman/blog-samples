using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.Search.Repositories.EntityFramework;

sealed class Customer
{
    public Customer(string companyName)
    {
        CompanyName = companyName;
    }

    public long Id { get; [UsedImplicitly] private set; }

    [Required]
    [MaxLength(50)]
    public string CompanyName { get; [UsedImplicitly] private set; }

    [MinLength(1), MaxLength(24)]
    [Phone]
    public string? PhoneNumber { get; set; }

    [ConcurrencyCheck]
    public long Version { get; set; }
}
