using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Northwind.Foundation;

namespace CustomerManagement.Search.Api;

[DataContract]
public class CustomerSearchByCompanyNameRequest : IValidatableObject
{
    [DataMember(Order = 1)]
    public string CompanyName { get; set; } = string.Empty;

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(CompanyName) && CompanyName != "%";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!IsValid())
            return new[] { new ValidationResult("CompanyName must be provided and cannot be a wildcard.") };
        
        return Enumerable.Empty<ValidationResult>();
    }

    public IReadOnlyCollection<Error> Validate()
    {
        return Validate(null!)
            .Select(v => new Error(
                "VALIDATION_ERROR", 
                v.ErrorMessage!))
            .ToArray();
    }
}