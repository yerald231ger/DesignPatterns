namespace Builder;

public class Station(int stationId, string name, string description)
{
    public int StationId { get; set; } = stationId;
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Pl { get; set; } = string.Empty;
    public List<(int Id, string Product)> Tanks { get; } = [];
    public List<(int Id, string Product)> Pumps { get; } = [];

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

        Pumps.Add((pumpId, product));
        return true;
    }
    
    public void DisplayStationInfo()
    {
        Console.WriteLine($"Station ID: {StationId}, Name: {Name}, Description: {Description}, Latitude: {Latitude}, Longitude: {Longitude}, PL: {Pl}");
        Console.WriteLine("Tanks:");
        foreach (var tank in Tanks)
        {
            Console.WriteLine($"Tank ID: {tank.Id}, Product: {tank.Product}");
        }
        Console.WriteLine("Pumps:");
        foreach (var pump in Pumps)
        {
            Console.WriteLine($"Pump ID: {pump.Id}, Product: {pump.Product}");
        }
    }
}