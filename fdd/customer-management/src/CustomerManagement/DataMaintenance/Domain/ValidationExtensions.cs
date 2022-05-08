using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.CustomerManagement.DataMaintenance.Domain;

static class ValidationExtensions
{
    internal static IList<ValidationResult> Validate(this object source)
    {
        ValidationContext valContext = new ValidationContext(source, null, null);
        var result = new List<ValidationResult>();
        Validator.TryValidateObject(source, valContext, result, true);

        return result;
    }
}
