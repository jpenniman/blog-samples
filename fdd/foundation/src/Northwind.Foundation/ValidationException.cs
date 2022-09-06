using System.ComponentModel.DataAnnotations;

namespace Northwind.CustomerManagement.Api;

/// <summary>
/// Thrown when model validation fails.
/// </summary>
public class ValidationException : ApplicationException
{
    /// <summary>
    /// Initializes a new instance.
    /// </summary>
    /// <param name="errors"></param>
    public ValidationException(IEnumerable<ValidationResult> errors)
        : base("Invalid")
    {
        Errors = errors;
    }

    /// <summary>
    /// A collection of validation errors.
    /// </summary>
    IEnumerable<ValidationResult> Errors { get; }
}