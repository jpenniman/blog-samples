using Northwind.CustomerManagement.Microservice;

var builder = WebApplication.CreateBuilder(args);
builder.RegisterComponents();
var app = builder.Build();

app.UseRouting();
app.UseEndpoints(ep => ep.MapGraphQL());


app.Run();