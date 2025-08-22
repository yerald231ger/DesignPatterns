using Builder.Pattern;
using Builder.Data;
using Builder.Builders;

Console.WriteLine("=== Advanced Builder Pattern Demo - OxxoGas Station Management ===\n");

var directors = GasStationDirectorFactory.GetAllDirectors();

Console.WriteLine("Choose the type of gas station to build:");
for (int i = 0; i < directors.Count; i++)
{
    Console.WriteLine($"{i + 1}. {directors[i].GetStationType()}");
}
Console.WriteLine($"{directors.Count + 1}. Test Station with Historical Data");

Console.Write("\nEnter your choice (1-5): ");
var choice = int.TryParse(Console.ReadLine(), out var result) ? result : 1;

IGasStationDirector selectedDirector;

if (choice == directors.Count + 1)
{
    // Test station with sample data
    Console.WriteLine("\nGenerating sample reception and delivery data...");
    var receptionInventories = SampleDataGenerator.GenerateReceptionInventories();
    var deliveryInventories = SampleDataGenerator.GenerateDeliveryInventories();
    
    selectedDirector = GasStationDirectorFactory.CreateTestDirector(receptionInventories, deliveryInventories);
    Console.WriteLine($"✓ Generated {receptionInventories.Length} reception records");
    Console.WriteLine($"✓ Generated {deliveryInventories.Length} delivery transactions");
}
else
{
    selectedDirector = choice > 0 && choice <= directors.Count 
        ? directors[choice - 1] 
        : directors[0];
}

Console.WriteLine($"\n🏗️  Building: {selectedDirector.GetStationType()}");
Console.WriteLine(new string('-', 60));

var station = selectedDirector.BuildGasStation();

Console.WriteLine("✅ Station construction completed!");

SampleDataGenerator.DisplayStationSummary(station);

Console.WriteLine("\n=== Builder Pattern Benefits Demonstrated ===");
Console.WriteLine("✓ Complex object construction with multiple components");
Console.WriteLine("✓ Different directors create different station configurations");
Console.WriteLine("✓ Fluent interface for readable construction code");
Console.WriteLine("✓ Separation of construction logic from business objects");
Console.WriteLine("✓ Consistent construction process across different station types");

Console.WriteLine("\n📊 Try different station types to see how the Builder pattern");
Console.WriteLine("   enables flexible construction of complex gas station configurations!");

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();