namespace FactoryMethod;

public abstract class Tank
{
    public abstract string TankType { get; }
    public abstract int Capacity { get; }

    public required int TankId { get; init; }
    public required Product Product { get; init; }

    public decimal CurrentTemperature { get; set; }
    public int CurrentFuelLevel { get; protected set; }
    public decimal CurrentFuelVolume { get; protected set; }

    protected decimal CompensateTemperature()
    {
        var r1 = 20m - CurrentTemperature;
        var r2 = CurrentFuelVolume * Product.ExpansionCoefficient * r1;
        var compensatedVolume = CurrentFuelVolume + r2;
        return compensatedVolume;
    }

    public abstract void SetCurrentFuelLevel(int fuelLevel);
    public abstract Dictionary<int, decimal> GetCalibrationTable();
    public abstract void WriteToConsole();
}

public struct Product()
{
    public ProductType Type { get; } = ProductType.Magna;
    public decimal ExpansionCoefficient { get; } = 0.00098m;

    public Product(ProductType type, decimal expansionCoefficient) : this()
    {
        Type = type;
        ExpansionCoefficient = expansionCoefficient;
    }

    public enum ProductType
    {
        Magna,
        Premium,
        Diesel,
        Urea
    }
}

public class HorizontalTank : Tank
{
    readonly Dictionary<int, decimal> _tableCalibrations = new()
    {
        { 1, 167m },
        { 2, 334m },
        { 3, 500m }
    };

    public override string TankType => "Horizontal";
    public override int Capacity => 550;

    public override string ToString()
    {
        return $"This is a Horizontal Tank with Volume: {CurrentFuelVolume} liters.";
    }

    public override void SetCurrentFuelLevel(int fuelLevel)
    {
        if (_tableCalibrations.TryGetValue(fuelLevel, out var currentFuelVolume))
        {
            CurrentFuelLevel = fuelLevel;
            CurrentFuelVolume = currentFuelVolume;
            CurrentFuelVolume = CompensateTemperature();
        }
        else
            Console.WriteLine($"Current fuel level {CurrentFuelLevel} is not supported");
    }

    public override Dictionary<int, decimal> GetCalibrationTable()
    {
        return new Dictionary<int, decimal>(_tableCalibrations);
    }

    public override void WriteToConsole()
    {
        var width = 50;
        var height = 10;
        var fillLevel = (int)(10m / Capacity * CurrentFuelVolume);
        if (fillLevel < 1 || fillLevel > 10)
        {
            Console.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        int fillHeight = (int)(height * (fillLevel / 10.0)); // Compute how many rows to fill

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    Console.Write("*"); // Outline of the rectangle
                }
                else if (y >= height - fillHeight)
                {
                    Console.Write("#"); // Filling from bottom to top
                }
                else
                {
                    Console.Write(" "); // Empty space
                }
            }

            Console.WriteLine();
        }
        Console.WriteLine($"Max Capacity: {Capacity} liters");
        Console.WriteLine($"Current Volume: {CurrentFuelVolume} liters");
        Console.WriteLine();
        Console.WriteLine();
    }
}

public class VerticalTank : Tank
{
    readonly Dictionary<int, decimal> _tableCalibrations = new()
    {
        { 1, 100m },
        { 2, 200m },
        { 3, 300m },
        { 4, 400m },
        { 5, 500m }
    };

    public override string TankType => "Vertical";
    public override int Capacity => 500;

    public override string ToString()
    {
        return $"This is a Vertical Tank with Volume: {CurrentFuelVolume} liters.";
    }

    public override void SetCurrentFuelLevel(int fuelLevel)
    {
        if (_tableCalibrations.TryGetValue(fuelLevel, out var currentFuelVolume))
        {
            CurrentFuelLevel = fuelLevel;
            CurrentFuelVolume = currentFuelVolume;
            CurrentFuelVolume = CompensateTemperature();
        }
        else
            Console.WriteLine($"Current fuel level {CurrentFuelLevel} is not supported");
    }

    public override Dictionary<int, decimal> GetCalibrationTable()
    {
        return new Dictionary<int, decimal>(_tableCalibrations);
    }

    public override void WriteToConsole()
    {
        var width = 20;
        var height = 10;
        var fillLevel = (int)(10m / Capacity * CurrentFuelVolume);
        if (fillLevel < 1 || fillLevel > 10)
        {
            Console.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        int fillHeight = (int)(height * (fillLevel / 10.0)); // Compute how many rows to fill

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    Console.Write("*"); // Outline of the rectangle
                }
                else if (y >= height - fillHeight)
                {
                    Console.Write("#"); // Filling from bottom to top
                }
                else
                {
                    Console.Write(" "); // Empty space
                }
            }

            Console.WriteLine();
        }
        Console.WriteLine($"Max Capacity: {Capacity} liters");
        Console.WriteLine($"Current Volume: {CurrentFuelVolume} liters");
        Console.WriteLine();
        Console.WriteLine();
    }
}

public class SphericalTank : Tank
{
    readonly Dictionary<float, decimal> _tableCalibrations = new()
    {
        { 1, 25m },
        { 1.5f, 50m },
        { 2, 125m },
        { 2.5f, 200m },
        { 3, 300m },
        { 3.5f, 375m },
        { 4, 475m },
        { 4.5f, 500m },
        { 5, 500m }
    };

    public override string TankType => "Spherical";

    public override int Capacity => 510;

    public override string ToString()
    {
        return $"This is a Spherical Tank with Volume: {CurrentFuelVolume} liters.";
    }

    public override void SetCurrentFuelLevel(int fuelLevel)
    {
        if (_tableCalibrations.Max(t => t.Key) < fuelLevel)
            Console.WriteLine($"Current fuel level {CurrentFuelLevel} is not supported");

        //find the nearest calibration
        var nearestCalibration = _tableCalibrations.OrderBy(x => Math.Abs(x.Key - fuelLevel)).First();

        CurrentFuelLevel = fuelLevel;
        CurrentFuelVolume = _tableCalibrations[nearestCalibration.Key];
        CurrentFuelVolume = CompensateTemperature();
    }

    public override Dictionary<int, decimal> GetCalibrationTable()
    {
        return _tableCalibrations.ToDictionary(k => (int)k.Key, v => v.Value);
    }

    public override void WriteToConsole()
    {
        var radius = 7;
        var fillLevel = (int)(20m / Capacity * CurrentFuelVolume);

        double aspectRatio = 2.0; // Adjust for console character proportions
        int diameter = radius * 2;

        // Compute the threshold Y level where filling should start (from bottom)
        int fillThresholdY = (int)(radius - (radius * (fillLevel / 10.0)));

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -diameter; x <= diameter; x++)
            {
                double distance = Math.Pow(x / aspectRatio, 2) + Math.Pow(y, 2);
                double outerThreshold = Math.Pow(radius + 0.5, 2);
                double innerThreshold = Math.Pow(radius - 0.5, 2);

                if (distance >= innerThreshold && distance <= outerThreshold)
                {
                    Console.Write("*"); // Circle outline
                }
                else if (distance < innerThreshold && y >= fillThresholdY)
                {
                    Console.Write("#"); // Fill progressively from bottom
                }
                else
                {
                    Console.Write(" "); // Empty space
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Max Capacity: {Capacity} liters");
        Console.WriteLine($"Current Volume: {CurrentFuelVolume} liters");
        Console.WriteLine();
        Console.WriteLine();
    }
}