namespace Flywight.Pattern;

public class FuelTypeFlyweightFactory
{
    private static readonly Dictionary<FuelType, IFuelTypeFlyweight> _flyweights = new();

    public static IFuelTypeFlyweight GetFuelTypeFlyweight(FuelType fuelType)
    {
        if (_flyweights.TryGetValue(fuelType, out var existingFlyweight))
        {
            return existingFlyweight;
        }

        var flyweight = fuelType switch
        {
            FuelType.Premium => new FuelTypeFlyweight("Premium Gasoline", "PRM", "Gold"),
            FuelType.Magna => new FuelTypeFlyweight("Magna Gasoline", "MAG", "Green"),
            FuelType.Diesel => new FuelTypeFlyweight("Diesel Fuel", "DSL", "Blue"),
            _ => throw new ArgumentException($"Unknown fuel type: {fuelType}")
        };

        _flyweights[fuelType] = flyweight;
        return flyweight;
    }

    public static int GetFlyweightCount()
    {
        return _flyweights.Count;
    }

    public static void ClearCache()
    {
        _flyweights.Clear();
    }
}

public enum FuelType
{
    Premium,
    Magna,
    Diesel
}