using PersonService.Domain;

namespace PersonService.Repositories;

public interface IRepository
{
    IEnumerable<Person> GetPeople();
}