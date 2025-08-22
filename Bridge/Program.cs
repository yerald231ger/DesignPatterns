using Bridge;
using Bridge.Pattern;

Console.WriteLine("=== Bridge Pattern Demo - Tank Display System ===\n");

// Demo 1: Standard tank with console renderer
Console.WriteLine("1. Standard Tank Display with Console Renderer:");
var consoleRenderer = new ConsoleTankRenderer();
var standardTank = new StandardTankDisplay(consoleRenderer, "T001", "Premium", 1000m);
standardTank.SetVolume(450m);
standardTank.Display();

// Demo 2: Detailed tank with console renderer
Console.WriteLine("2. Detailed Tank Display with Console Renderer:");
var detailedTank = new DetailedTankDisplay(consoleRenderer, "T002", "Diesel", 2000m);
detailedTank.SetVolume(150m); // Low fuel
detailedTank.Display();

// Demo 3: Standard tank with file renderer
Console.WriteLine("3. Standard Tank Display with File Renderer:");
var fileRenderer = new FileTankRenderer();
var fileTank = new StandardTankDisplay(fileRenderer, "T003", "Magna", 1500m);
fileTank.SetVolume(1200m);
fileTank.Display();
Console.WriteLine("Tank report saved to file: tank_reports/tank_T003.txt");

// Demo 4: Detailed tank with JSON renderer
Console.WriteLine("\n4. Detailed Tank Display with JSON Renderer:");
var jsonRenderer = new JsonTankRenderer();
var jsonTank = new DetailedTankDisplay(jsonRenderer, "T004", "Premium", 800m);
jsonTank.SetVolume(790m); // High fuel warning
jsonTank.Display();
Console.WriteLine("Tank data saved to JSON: tank_json/tank_T004.json");

Console.WriteLine("\n=== Bridge Pattern Benefits ===");
Console.WriteLine("✓ Abstraction (TankDisplay) separated from Implementation (ITankRenderer)");
Console.WriteLine("✓ Can extend displays: StandardTankDisplay, DetailedTankDisplay");
Console.WriteLine("✓ Can extend renderers: ConsoleRenderer, FileRenderer, JsonRenderer");
Console.WriteLine("✓ Any display type can work with any renderer type");