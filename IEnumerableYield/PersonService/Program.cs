using System.Data;
using PersonService.Repositories;
using Microsoft.Data.Sqlite;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddTransient<IDbConnection>(_ => new SqliteConnection("Filename=people.db"));
builder.Services.AddTransient<IRepository, SqlRepository>();
    
var app = builder.Build();

Seed(app);

app.MapControllers();

app.Run();

// Clean up the demo
File.Delete("people.db");

// Create and populate the table
void Seed(WebApplication app)
{
    using var cn = app.Services.GetRequiredService<IDbConnection>();
    cn.Open();
    using var cmd = cn.CreateCommand();
    cmd.CommandText = "create table person (id int not null primary key, name varchar(128), email varchar(128))";
    cmd.ExecuteNonQuery();
    cmd.CommandText = @"insert into person (id, name, email)
                        values (1, 'Elmer Fudd', 'efudd@acme.com'),
                               (2, 'Homer Simpson', 'hsimpson@acme.com'),
                               (3, 'Bruce Wayne', 'batman@acme.com'),
                               (4, 'Barbara Gordon', 'batgirl@acme.com'),
                               (5, 'Diana Prince', 'dprince@acme.com')";
    cmd.ExecuteNonQuery();
}