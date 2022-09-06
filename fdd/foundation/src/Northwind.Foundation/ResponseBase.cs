using System.Runtime.Serialization;

namespace Northwind.Foundation;

public abstract class ResponseBase<T>
{
    [DataMember(Order = 1)]
    public T Data { get; protected set; } = default!;

    [DataMember(Order = 2)]
    public IReadOnlyCollection<Error> Errors { get; protected set; } = ArraySegment<Error>.Empty;

    public bool IsSuccessful => !Errors.Any();
}