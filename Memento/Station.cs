using System.Text.Json;
using Memento.Pattern;

namespace Memento;

public class Station(string name) : IMemento<Station.StationMemento>
{
    public List<Tank> Tanks { get; set; } = [];
    public List<Dispenser> Dispensers { get; set; } = [];
    public string Name { get; set; } = name;

    public void AddTank(Tank tank)
    {
        if (Tanks.Any(t => t.Code == tank.Code))
        {
            throw new Exception("Tank already exists");
        }

        Tanks.Add(tank);
    }

    public void RemoveTank(Tank tank)
    {
        Tanks.Remove(tank);
    }

    public void AddDispenser(Dispenser dispenser)
    {
        if (Dispensers.Any(d => d.Code == dispenser.Code))
        {
            throw new Exception("Dispenser already exists");
        }

        Dispensers.Add(dispenser);
    }

    public void RemoveDispenser(Dispenser dispenser)
    {
        Dispensers.Remove(dispenser);
    }

    public StationMemento Save()
    {
        var station = JsonSerializer.Serialize(this);
        return new StationMemento(station);
    }

    public void Restore(StationMemento memento)
    {
        var station = JsonSerializer.Deserialize<Station>(memento.State)!;

        Name = station.Name;
        Tanks = station.Tanks;
        Dispensers = station.Dispensers;
    }

    public record struct StationMemento(string State);
}

public class Tank
{
    public string Code { get; init; }
    public decimal Volume { get; set; }
    public Product Product { get; set; }
}

public class Dispenser(string code)
{
    public string Code { get; } = code;
    public List<Pump> Pumps { get; set; } = [];
}

public class Pump(string code)
{
    public string Code { get; } = code;
    public List<Hose> Hoses { get; set; } = [];
}

public class Hose(string code)
{
    public string Code { get; } = code;
    public Tank? Tank { get; set; }
}
public enum Product
{
    Magna,
    Premium,
    Diesel
}