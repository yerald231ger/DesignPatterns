using AbstractFactory.Pattern;
using AbstractFactory.Reporters;

var type = typeof(ConsoleReporter);
var factories = IReporterFactory.GetFactories();

Console.WriteLine("What kind of tank do you want to create? choose a number:");
factories.ForEach(f => Console.WriteLine($"{f.GetType().Name} -> {factories.IndexOf(f) + 1}"));


var result = int.TryParse(Console.ReadLine(), out var choice) ? choice : 0;
var reporterFactory = factories.ElementAt(result - 1);
Console.Clear();
Run(reporterFactory);

return;

static void Run(IReporterFactory reporterFactory)
{
    var tankReporter = reporterFactory.CreateTankReporter();
    var pumpReporter = reporterFactory.CreatePumpReporter();
    
    tankReporter.Write(1, "Magna", 500, 1000);
    pumpReporter.Write(1, "Magna", "Premium", 500, 1000);
}