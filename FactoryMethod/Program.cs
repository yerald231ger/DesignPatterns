using System.Reflection;
using FactoryMethod.Pattern;

var factories = TankConsoleWriter.GetTankConsoleWriters();

Console.WriteLine("What kind of tank do you want to create? choose a number:");
factories.ForEach(f => Console.WriteLine($"{f.GetType().Name} -> {factories.IndexOf(f) + 1}"));


var result = int.TryParse(Console.ReadLine(), out var choice) ? choice : 0;
var consoleWrite = factories.ElementAt(result - 1);
Console.Clear();

consoleWrite.Write(1, "Magna", 500, 1000);
consoleWrite.Write(2, "Premium", 800, 1000);
consoleWrite.Write(3, "Diesel", 200, 1000);