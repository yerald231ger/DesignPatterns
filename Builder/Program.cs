using Builder.Pattern;

var builders = IStationBuilder.GetBuilders();

Console.WriteLine("What kind of station do you want to create? choose a number:");
builders.ForEach(f => Console.WriteLine($"{f.GetType().Name} -> {builders.IndexOf(f) + 1}"));

var result = int.TryParse(Console.ReadLine(), out var choice) ? choice : 0;
var reporterFactory = builders.ElementAt(result - 1);
Console.Clear();

var director = new StationDirector();

Console.WriteLine("Choose a stations director: 1 -> Simple Order Station, 2 -> Random Steps Station");
var directorChoice = int.TryParse(Console.ReadLine(), out choice) ? choice : 0;

if (directorChoice == 1)
    director.BuildSimpleOrderStation(reporterFactory);
else
    director.BuildRandomStepsStation(reporterFactory);

var station = reporterFactory.Build();

station.DisplayStationInfo();