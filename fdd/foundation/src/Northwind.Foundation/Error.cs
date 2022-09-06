using JetBrains.Annotations;

namespace Northwind.Foundation;

/// <summary>
/// Error objects provide additional information about problems encountered while performing an operation.
/// </summary>
[PublicAPI]
[Serializable]
public sealed class Error
{
#pragma warning disable CS8618 // Initialize non-nullable properties
    [UsedImplicitly]
    Error()
    {
        // Required for serialization
    }
#pragma warning restore CS8618

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code">an application-specific error code, expressed as a string value.</param>
    /// <param name="message">a short, human-readable summary of the problem that SHOULD NOT change from occurrence to occurrence of the problem, except for purposes of localization.</param>
    /// <param name="metadata">a meta object containing non-standard meta-information about the error.</param>
    public Error(string code, string message, IReadOnlyCollection<KeyValuePair<string, object>>? metadata = null)
    {
        Id = Guid.NewGuid();
        Code = code;
        Message = message;
        Metadata = metadata ?? ArraySegment<KeyValuePair<string, object>>.Empty;
    }

    public Guid Id { get; private set; }

    public string Code { get; private set; }

    public string Message { get; private set; }
    
    public IReadOnlyCollection<KeyValuePair<string, object>> Metadata { get; private set; }
}
