using System.Reflection;

namespace FactoryMethod.Pattern;

public abstract class TankConsoleWriter
{
    protected abstract IConsoleShapeWriter CreateConsoleShapeWriter();
    
    public void Write(int id, string product, decimal volume, decimal capacity)
    {
        Console.WriteLine("───────────────────────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine($"Tanque {id} de {product}".ToUpper());
        var writer = CreateConsoleShapeWriter();
        var fillLevel = (int)(10m / capacity * volume);
        writer.Write(fillLevel);
        Console.WriteLine($"Max Capacity: {capacity} liters");
        Console.WriteLine($"Current Volume: {volume} liters");
        Console.WriteLine();
    }
    
    public static List<TankConsoleWriter> GetTankConsoleWriters()
    {
        return Assembly.GetCallingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(TankConsoleWriter)))
            .Select(t => (TankConsoleWriter)Activator.CreateInstance(t)!)
            .ToList();
    }
}

public class HorizontalTankConsoleWriter : TankConsoleWriter
{
    protected override IConsoleShapeWriter CreateConsoleShapeWriter()
    {
        var width = 40;
        var height = 10;
        return new ConsoleShapeHorizontalWriter(height, width);
    }
}

public class VerticalTankConsoleWriter : TankConsoleWriter
{
    protected override IConsoleShapeWriter CreateConsoleShapeWriter()
    {
        var width = 25;
        var height = 14;
        return new ConsoleShapeVerticalWriter(height, width);
    }
}

public class SphericalTankConsoleWriter : TankConsoleWriter
{
    protected override IConsoleShapeWriter CreateConsoleShapeWriter()
    {
        var radius = 7;
        return new ConsoleShapeSphericalWriter(radius);
    }
}

public class VerticalColorizedTankConsoleWriter : TankConsoleWriter
{
    protected override IConsoleShapeWriter CreateConsoleShapeWriter()
    {
        var width = 25;
        var height = 14;
        return new ConsoleShapeVerticalColorizedWriter(height, width);
    }
}

public class RandomShapeHorizontalWriter : TankConsoleWriter
{
    protected override IConsoleShapeWriter CreateConsoleShapeWriter()
    {
        var random = new Random();
        var next = random.Next(1, 4);
        return next switch
        {
            1 => new ConsoleShapeHorizontalWriter(10, 40),
            2 => new ConsoleShapeVerticalWriter(14, 25),
            _ => new ConsoleShapeSphericalWriter(7)
        };
    }
}
