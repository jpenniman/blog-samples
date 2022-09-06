using System;

namespace Northwind.ShippingManagement.Rates.Domain;

class Rate
{
    public Rate(string country, decimal flatRate)
    {
        Country = country;
        FlatRate = flatRate;
    }
    
    public long Id { get; private set; }

    public string Country { get; set; }

    public decimal FlatRate { get; set; }

    public long Version { get; set; } = 1;

    public Guid ObjectId { get; set; } = Guid.NewGuid();
}