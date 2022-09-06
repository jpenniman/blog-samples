using Microsoft.EntityFrameworkCore;
using Northwind.ShippingManagement.Grpc.Server;
using Northwind.ShippingManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShippingManagement(options =>
{
    var cnString = builder.Configuration["ShippingManagement:DatabaseConnection"];
    options.UseNpgsql(cnString);
});
builder.Services.AddShippingServiceGrpcServer();

var app = builder.Build();
app.UseRouting();
app.MapShippingManagementGrpcServices();

app.Run();