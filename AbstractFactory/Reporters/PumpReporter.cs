using AbstractFactory.Pattern;

namespace AbstractFactory.Reporters;

public class PumpReporter : IPumpReporter
{
    public void Write(int id, string productOne, string productTwo, decimal productOneVolume, decimal productTwoVolume)
    {
        Console.WriteLine("───────────────────────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine($"Pump {id} with products {productOne} and {productTwo}");
        Console.WriteLine($"Product one volume: {productOneVolume} liters");
        Console.WriteLine($"Product two volume: {productTwoVolume} liters");
    }
}

public class FilePumpReporter : IPumpReporter
{
    public void Write(int id, string productOne, string productTwo, decimal productOneVolume, decimal productTwoVolume)
    {
        using var writer = new StreamWriter($"pump_{id}.txt");
        writer.WriteLine("───────────────────────────────────────────────────────────────");
        writer.WriteLine();
        writer.WriteLine($"Pump {id} with products {productOne} and {productTwo}");
        writer.WriteLine($"Product one volume: {productOneVolume} liters");
        writer.WriteLine($"Product two volume: {productTwoVolume} liters");
    }
}