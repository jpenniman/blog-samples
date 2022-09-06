namespace Northwind.Foundation;

public abstract class PagedResponseBase<T> : ResponseBase<IReadOnlyCollection<T>>
{
    public int PageSize { get; protected set; }

    public int TotalCount { get; protected set; }
}