using System.Data;
using PersonService.Domain;

namespace PersonService.Repositories;

class SqlRepository : IRepository
{
    readonly IDbConnection _connection;

    public SqlRepository(IDbConnection connection)
    {
        _connection = connection;
        _connection.Open();
    }

    public IEnumerable<Person> GetPeople()
    { 
        using var cmd = _connection.CreateCommand();
        cmd.CommandText = "select id, name, email from person";

        using var rdr = cmd.ExecuteReader();
        while (rdr.Read())
            yield return new Person(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2));
    }
}