using System.Linq.Expressions;

namespace Northwind.Foundation.Repositories;

public interface IReadRepository<TEntity, TKey> where TEntity : class
{
    ValueTask<TEntity?> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);
    IAsyncEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter, int skip, int take, CancellationToken cancellationToken = default);
}