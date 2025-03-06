namespace Bridge;

public class Tank(ITankType tankType)
{
    public decimal Volume { get; private set; }
    public decimal Temperature { get; set; }
    public int FuelLevel { get; private set; }
    public ITankType TankType { get; } = tankType;

    public void SetFuelLevel(int fuelLevel)
    {
        FuelLevel = fuelLevel;
        Volume = TankType.GetVolume(fuelLevel);
    }
    
    public void DisplayTankInfo()
    {
        Console.WriteLine($"Fuel Level: {FuelLevel}, Volume: {Volume} liters.");
    }
}

file class HorizontalTankType(Dictionary<int, decimal> calibrationTable) : ITankType
{
    private Dictionary<int, decimal> CalibrationTable { get; } = calibrationTable;
    public string Name { get; set; } = nameof(HorizontalTankType);
    public decimal Height { get; set; }
    public decimal Diameter { get; set; }

    public decimal GetVolume(int fuelLevel)
    {
        return CalibrationTable[fuelLevel];
    }
}

file class VerticalTankType(Dictionary<int, decimal> calibrationTable) : ITankType
{
    private Dictionary<int, decimal> CalibrationTable { get; } = calibrationTable;
    public string Name { get; set; } = nameof(VerticalTankType);
    public decimal Height { get; set; }
    public decimal Diameter { get; set; }

    public decimal GetVolume(int fuelLevel)
    {
        return CalibrationTable[fuelLevel];
    }
}

public interface ITankType
{
    public abstract decimal GetVolume(int fuelLevel);
}

public static class TankFactory
{
    public static ITankType GetTankType(string tankType)
    {
        return tankType switch
        {
            "Horizontal" => new HorizontalTankType(new Dictionary<int, decimal> { { 1, 200.1m }, { 2, 300m }, { 3, 400m } }),
            "Vertical" => new VerticalTankType(new Dictionary<int, decimal> { { 1, 100.1m }, { 2, 200m }, { 3, 300m } }),
            _ => throw new ArgumentOutOfRangeException(nameof(tankType), tankType, null)
        };
    }
}