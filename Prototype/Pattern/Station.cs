using System.Text;

namespace Prototype.Pattern;

public class Station
{
    public int StationId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Pl { get; set; }
    public List<(int Id, string Product)> Tanks { get; private set; } = [];
    public List<(int Id, string Product)> Pumps { get; private set; } = [];

    public Station()
    {
        Name = string.Empty;
        Description = string.Empty;
        Pl = string.Empty;
    }

    public void Deconstruct(out List<(int Id, string Product)> tanks, out List<(int Id, string Product)> pumps)
    {
        tanks = Tanks;
        pumps = Pumps;
    }

    public bool AddTank(int tankId, string product)
    {
        if (string.IsNullOrWhiteSpace(product))
            throw new ArgumentException("Product cannot be empty", nameof(product));

        if (tankId <= 0)
            throw new ArgumentException("Tank ID must be greater than zero", nameof(tankId));

        if (Tanks.Any(t => t.Id == tankId))
            throw new InvalidOperationException($"Tank with ID {tankId} already exists");

        Tanks.Add((tankId, product));
        return true;
    }

    public bool AddPump(int pumpId, string product)
    {
        if (string.IsNullOrWhiteSpace(product))
            throw new ArgumentException("Product cannot be empty", nameof(product));

        if (pumpId <= 0)
            throw new ArgumentException("Pump ID must be greater than zero", nameof(pumpId));

        if (Pumps.Any(p => p.Id == pumpId))
            throw new InvalidOperationException($"Pump with ID {pumpId} already exists");

        if (!Tanks.Any(t => t.Product == product))
            throw new InvalidOperationException($"No tank exists with product {product}");

        Pumps.Add((pumpId, product));
        return true;
    }

    public Station Clone()
    {
        var station = new Station
        {
            Tanks = Tanks,
            Pumps = Pumps
        };
        return station;
    }

    public override string ToString()
    {
        var stationInfo = new StringBuilder();
        stationInfo.AppendLine($"Station ID: {StationId}");
        stationInfo.AppendLine($"Name: {Name}");
        stationInfo.AppendLine($"Description: {Description}");
        stationInfo.AppendLine($"Location: ({Latitude}, {Longitude})");
        stationInfo.AppendLine($"PL: {Pl}");
    
        stationInfo.AppendLine("\nTanks:");
        foreach (var tank in Tanks)
        {
            stationInfo.AppendLine($"- ID: {tank.Id}, Product: {tank.Product}");
        }
    
        stationInfo.AppendLine("\nPumps:");
        foreach (var pump in Pumps)
        {
            stationInfo.AppendLine($"- ID: {pump.Id}, Product: {pump.Product}");
        }
    
        return stationInfo.ToString();
    }
}