namespace Northwind.Foundation.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}