using Flywight;
using Flywight.Pattern;
using System.Diagnostics;

Console.WriteLine("=== Flyweight Pattern Demo - Fuel Sales Memory Optimization ===\n");

const int SaleCount = 1_000_000;

// Demo 1: Memory usage comparison
Console.WriteLine("1. Memory Usage Comparison:");
MemoryUsageDemo(SaleCount);

// Demo 2: Performance comparison
Console.WriteLine("\n2. Performance Comparison:");
PerformanceDemo(SaleCount);

// Demo 3: Flyweight sharing demonstration
Console.WriteLine("\n3. Flyweight Sharing Demonstration:");
FlyweightSharingDemo();

static void MemoryUsageDemo(int count)
{
    var initialMemory = GC.GetTotalMemory(true);
    
    // Without Flyweight
    var salesWithoutFlyweight = FuelSaleWithoutFlyweight.GenerateSalesWithoutFlyweight(count);
    var memoryWithoutFlyweight = GC.GetTotalMemory(true) - initialMemory;
    
    GC.Collect();
    GC.WaitForPendingFinalizers();
    GC.Collect();
    
    var initialMemory2 = GC.GetTotalMemory(true);
    
    // With Flyweight
    var salesWithFlyweight = FuelSaleWithFlyweight.GenerateSalesWithFlyweight(count);
    var memoryWithFlyweight = GC.GetTotalMemory(true) - initialMemory2;
    
    Console.WriteLine($"Sales generated: {count:N0}");
    Console.WriteLine($"Memory without Flyweight: {memoryWithoutFlyweight / 1024.0 / 1024.0:F2} MB");
    Console.WriteLine($"Memory with Flyweight: {memoryWithFlyweight / 1024.0 / 1024.0:F2} MB");
    Console.WriteLine($"Memory saved: {(memoryWithoutFlyweight - memoryWithFlyweight) / 1024.0 / 1024.0:F2} MB");
    Console.WriteLine($"Memory reduction: {((double)(memoryWithoutFlyweight - memoryWithFlyweight) / memoryWithoutFlyweight) * 100:F1}%");
    Console.WriteLine($"Flyweight instances created: {FuelTypeFlyweightFactory.GetFlyweightCount()}");
}

static void PerformanceDemo(int count)
{
    var sw = Stopwatch.StartNew();
    var salesWithoutFlyweight = FuelSaleWithoutFlyweight.GenerateSalesWithoutFlyweight(count);
    sw.Stop();
    var timeWithoutFlyweight = sw.ElapsedMilliseconds;
    
    sw.Restart();
    var salesWithFlyweight = FuelSaleWithFlyweight.GenerateSalesWithFlyweight(count);
    sw.Stop();
    var timeWithFlyweight = sw.ElapsedMilliseconds;
    
    Console.WriteLine($"Time without Flyweight: {timeWithoutFlyweight} ms");
    Console.WriteLine($"Time with Flyweight: {timeWithFlyweight} ms");
    
    // Show some sample data
    Console.WriteLine("\nSample sales (with Flyweight):");
    for (int i = 0; i < Math.Min(3, salesWithFlyweight.Count); i++)
    {
        Console.WriteLine($"  {salesWithFlyweight[i].GetDisplayInfo()}");
    }
}

static void FlyweightSharingDemo()
{
    FuelTypeFlyweightFactory.ClearCache();
    
    Console.WriteLine("Creating fuel sales and observing flyweight sharing:");
    
    var sales = new List<FuelSaleWithFlyweight>();
    
    // Create sales for different fuel types
    for (int i = 0; i < 5; i++)
    {
        var context = new FuelSaleContext
        {
            SaleId = Guid.NewGuid(),
            SaleDateTime = DateTime.Now,
            PumpNumber = (short)(i + 1),
            HoseNumber = 1,
            Volume = 50,
            PricePerLiter = 24.50m,
            CustomerCode = $"CUST{i:D4}"
        };
        
        sales.Add(new FuelSaleWithFlyweight(FuelType.Premium, context));
        Console.WriteLine($"After sale {i + 1} (Premium): {FuelTypeFlyweightFactory.GetFlyweightCount()} flyweight(s) in memory");
    }
    
    // Add different fuel types
    var dieselContext = new FuelSaleContext
    {
        SaleId = Guid.NewGuid(),
        SaleDateTime = DateTime.Now,
        PumpNumber = 6,
        HoseNumber = 1,
        Volume = 60,
        PricePerLiter = 25.20m,
        CustomerCode = "CUST0006"
    };
    
    sales.Add(new FuelSaleWithFlyweight(FuelType.Diesel, dieselContext));
    Console.WriteLine($"After diesel sale: {FuelTypeFlyweightFactory.GetFlyweightCount()} flyweight(s) in memory");
    
    var magnaContext = new FuelSaleContext
    {
        SaleId = Guid.NewGuid(),
        SaleDateTime = DateTime.Now,
        PumpNumber = 7,
        HoseNumber = 1,
        Volume = 40,
        PricePerLiter = 22.30m,
        CustomerCode = "CUST0007"
    };
    
    sales.Add(new FuelSaleWithFlyweight(FuelType.Magna, magnaContext));
    Console.WriteLine($"After magna sale: {FuelTypeFlyweightFactory.GetFlyweightCount()} flyweight(s) in memory");
    
    Console.WriteLine($"\nTotal sales: {sales.Count}");
    Console.WriteLine($"Total flyweight instances: {FuelTypeFlyweightFactory.GetFlyweightCount()}");
    Console.WriteLine("✓ Only 3 flyweight objects for potentially millions of sales!");
}