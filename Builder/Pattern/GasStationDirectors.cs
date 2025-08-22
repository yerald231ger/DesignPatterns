using Builder.Models;
using Builder.Builders;

namespace Builder.Pattern;

public interface IGasStationDirector
{
    Builders.Station BuildGasStation();
    string GetStationType();
}

public class StandardGasStationDirector : IGasStationDirector
{
    public string GetStationType() => "Standard Gas Station";

    public Builders.Station BuildGasStation()
    {
        Console.WriteLine("Building Standard Gas Station with default configuration...");
        
        return new Builders.StationBuilder()
            .CreateDefault()
            .Build();
    }
}

public class HighVolumeGasStationDirector : IGasStationDirector
{
    public string GetStationType() => "High Volume Gas Station";

    public Builders.Station BuildGasStation()
    {
        Console.WriteLine("Building High Volume Gas Station with enhanced capacity...");
        
        var stationBuilder = new Builders.StationBuilder();
        var tankBuilder = new Builders.TankBuilder();

        // Create high-capacity tanks
        var magnaTank = tankBuilder
            .Default(p => p.ProductMagna)
            .WithNumber(1)
            .WithUtilCapacity(95000.000m) // Increased capacity
            .WithFirstAndLastTankReading(15000, 85000)
            .Build();

        var premiumTank = tankBuilder
            .Default(p => p.ProductPremium)
            .WithNumber(2)
            .WithUtilCapacity(95000.000m) // Increased capacity
            .WithFirstAndLastTankReading(15000, 85000)
            .Build();

        var dieselTank = tankBuilder
            .Default(p => p.ProductDiesel)
            .WithNumber(3)
            .WithUtilCapacity(150000.000m) // Much larger diesel tank
            .WithFirstAndLastTankReading(20000, 130000)
            .Build();

        // Create more dispensers for high volume
        
        var dispensers = new List<Dispenser>();
        
        // 4 gasoline dispensers (8 pumps total)
        for (short i = 1; i <= 4; i++)
        {
            var dispenser = new Builders.DispenserBuilder()
                .Default()
                .WithNumber(i)
                .AddPump(pb =>
                    pb.WithNumber((short)(i * 2 - 1))
                        .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                        .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
                )
                .AddPump(pb =>
                    pb.WithNumber((short)(i * 2))
                        .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                        .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
                ).Build();
            
            dispensers.Add(dispenser);
        }

        // 2 dedicated diesel dispensers
        for (short i = 5; i <= 6; i++)
        {
            var dieselDispenser = new Builders.DispenserBuilder()
                .Default()
                .WithNumber(i)
                .AddPump(pb =>
                    pb.WithNumber((short)(i + 4))
                        .AddHose(hb => hb.WithNumber(1).WithTankSuction(dieselTank))
                ).Build();
            
            dispensers.Add(dieselDispenser);
        }

        return new Builders.Station
        {
            Tanks = [magnaTank, premiumTank, dieselTank],
            Dispensers = dispensers,
            ErrorMargin = 0.03m // Tighter error margin for high volume
        };
    }
}

public class CompactGasStationDirector : IGasStationDirector
{
    public string GetStationType() => "Compact Gas Station";

    public Builders.Station BuildGasStation()
    {
        Console.WriteLine("Building Compact Gas Station for urban locations...");
        
        var tankBuilder = new Builders.TankBuilder();

        // Smaller tanks for compact station
        var magnaTank = tankBuilder
            .Default(p => p.ProductMagna)
            .WithNumber(1)
            .WithUtilCapacity(30000.000m) // Reduced capacity
            .WithFirstAndLastTankReading(5000, 25000)
            .Build();

        var premiumTank = tankBuilder
            .Default(p => p.ProductPremium)
            .WithNumber(2)
            .WithUtilCapacity(30000.000m) // Reduced capacity
            .WithFirstAndLastTankReading(5000, 25000)
            .Build();

        // Only one dispenser with both products
        var compactDispenser = new Builders.DispenserBuilder()
            .Default()
            .WithNumber(1)
            .AddPump(pb =>
                pb.WithNumber(1)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                    .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
            ).Build();

        return new Builders.Station
        {
            Tanks = [magnaTank, premiumTank], // No diesel for compact station
            Dispensers = [compactDispenser],
            ErrorMargin = 0.08m // Higher error margin acceptable for compact station
        };
    }
}

public class TruckStopDirector : IGasStationDirector
{
    public string GetStationType() => "Truck Stop Station";

    public Builders.Station BuildGasStation()
    {
        Console.WriteLine("Building Truck Stop Station optimized for heavy vehicles...");
        
        var tankBuilder = new Builders.TankBuilder();

        // Diesel-focused configuration
        var dieselTank1 = tankBuilder
            .Default(p => p.ProductDiesel)
            .WithNumber(1)
            .WithUtilCapacity(200000.000m) // Very large diesel tank
            .WithFirstAndLastTankReading(30000, 180000)
            .Build();

        var dieselTank2 = tankBuilder
            .Default(p => p.ProductDiesel)
            .WithNumber(2)
            .WithUtilCapacity(200000.000m) // Second large diesel tank
            .WithFirstAndLastTankReading(30000, 180000)
            .Build();

        // Smaller gasoline tanks (trucks occasionally need gasoline)
        var magnaTank = tankBuilder
            .Default(p => p.ProductMagna)
            .WithNumber(3)
            .WithUtilCapacity(40000.000m)
            .WithFirstAndLastTankReading(8000, 35000)
            .Build();

        var dispensers = new List<Dispenser>();

        // 3 diesel-only dispensers for trucks
        for (short i = 1; i <= 3; i++)
        {
            var dieselDispenser = new Builders.DispenserBuilder()
                .Default()
                .WithNumber(i)
                .AddPump(pb =>
                    pb.WithNumber(i)
                        .AddHose(hb => hb.WithNumber(1).WithTankSuction(i <= 2 ? dieselTank1 : dieselTank2))
                ).Build();
            
            dispensers.Add(dieselDispenser);
        }

        // 1 gasoline dispenser for regular vehicles
        var gasolineDispenser = new Builders.DispenserBuilder()
            .Default()
            .WithNumber(4)
            .AddPump(pb =>
                pb.WithNumber(4)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
            ).Build();
        
        dispensers.Add(gasolineDispenser);

        return new Builders.Station
        {
            Tanks = [dieselTank1, dieselTank2, magnaTank],
            Dispensers = dispensers,
            ErrorMargin = 0.02m // Very tight error margin for commercial operations
        };
    }
}

public class TestDataGasStationDirector : IGasStationDirector
{
    private readonly ReceptionInventory[] _receptionInventories;
    private readonly DetailedDeliveryInventory[] _deliveryInventories;

    public TestDataGasStationDirector(
        ReceptionInventory[] receptionInventories,
        DetailedDeliveryInventory[] deliveryInventories)
    {
        _receptionInventories = receptionInventories;
        _deliveryInventories = deliveryInventories;
    }

    public string GetStationType() => "Test Station with Historical Data";

    public Builders.Station BuildGasStation()
    {
        Console.WriteLine("Building Test Station with reception and delivery inventories...");
        
        return new Builders.StationBuilder()
            .CreateDefault()
            .LoadReceptionInventories(_receptionInventories)
            .LoadDeliveryInventories(_deliveryInventories)
            .Build();
    }
}

public static class GasStationDirectorFactory
{
    public static IGasStationDirector CreateDirector(StationType stationType)
    {
        return stationType switch
        {
            StationType.Standard => new StandardGasStationDirector(),
            StationType.HighVolume => new HighVolumeGasStationDirector(),
            StationType.Compact => new CompactGasStationDirector(),
            StationType.TruckStop => new TruckStopDirector(),
            _ => throw new ArgumentException($"Unknown station type: {stationType}")
        };
    }

    public static IGasStationDirector CreateTestDirector(
        ReceptionInventory[] receptionInventories,
        DetailedDeliveryInventory[] deliveryInventories)
    {
        return new TestDataGasStationDirector(receptionInventories, deliveryInventories);
    }

    public static List<IGasStationDirector> GetAllDirectors()
    {
        return new List<IGasStationDirector>
        {
            new StandardGasStationDirector(),
            new HighVolumeGasStationDirector(),
            new CompactGasStationDirector(),
            new TruckStopDirector()
        };
    }
}

public enum StationType
{
    Standard,
    HighVolume,
    Compact,
    TruckStop
}