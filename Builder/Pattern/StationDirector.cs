namespace Builder.Pattern;

public class StationDirector
{
    public void BuildSimpleOrderStation(IStationBuilder stationBuilder)
    {
        Console.WriteLine("Enter Station ID:");
        var stationId = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Enter Station Name:");
        var name = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter Station Description:");
        var description = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter Latitude:");
        var latitude = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Enter Longitude:");
        var longitude = double.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Enter PL:");
        var pl = Console.ReadLine() ?? string.Empty;

        var innerBuilder = stationBuilder
            .SetInitialData(stationId, name, description)
            .SetLocation(latitude, longitude)
            .SetPl(pl);

        Console.WriteLine("Enter number of tanks:");
        var tankCount = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < tankCount; i++)
        {
            Console.WriteLine($"Enter Tank {i + 1} ID:");

            Console.WriteLine($"Enter Tank {i + 1} Product:");
            var product = Console.ReadLine() ?? string.Empty;

            innerBuilder.AddTank(i + 1, product);
        }

        Console.WriteLine("Enter number of dispensers:");
        var dispenserCount = int.Parse(Console.ReadLine() ?? "0");

        for (var i = 0; i < dispenserCount; i++)
        {
            Console.WriteLine($"Enter Dispenser {i + 1} ID:");
            var dispenserId = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine($"Enter Dispenser {i + 1} Product:");
            var product = Console.ReadLine() ?? string.Empty;

            innerBuilder.AddDispenser(dispenserId, product);
        }
    }

    public void BuildRandomStepsStation(IStationBuilder stationBuilder)
    {
        var random = new Random();

        // Required steps (always executed in order)
        Console.WriteLine("Enter Station ID:");
        var stationId = int.Parse(Console.ReadLine() ?? "0");

        Console.WriteLine("Enter Station Name:");
        var name = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Enter Station Description:");
        var description = Console.ReadLine() ?? string.Empty;

        var innerBuilder = stationBuilder.SetInitialData(stationId, name, description);

        // Optional steps in random order
        var steps = new List<Action>
        {
            () =>
            {
                Console.WriteLine("Enter Latitude:");
                var latitude = double.Parse(Console.ReadLine() ?? "0");
                Console.WriteLine("Enter Longitude:");
                var longitude = double.Parse(Console.ReadLine() ?? "0");
                innerBuilder.SetLocation(latitude, longitude);
            },
            () =>
            {
                Console.WriteLine("Enter PL:");
                var pl = Console.ReadLine() ?? string.Empty;
                innerBuilder.SetPl(pl);
            },
            () =>
            {
                Console.WriteLine("Enter number of tanks:");
                var tankCount = int.Parse(Console.ReadLine() ?? "0");
                for (var i = 0; i < tankCount; i++)
                {
                    Console.WriteLine($"Enter Tank {i + 1} ID:");
                    var tankId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine($"Enter Tank {i + 1} Product:");
                    var product = Console.ReadLine() ?? string.Empty;
                    innerBuilder.AddTank(tankId, product);
                }
            },
            () =>
            {
                Console.WriteLine("Enter number of dispensers:");
                var dispenserCount = int.Parse(Console.ReadLine() ?? "0");
                for (var i = 0; i < dispenserCount; i++)
                {
                    Console.WriteLine($"Enter Dispenser {i + 1} ID:");
                    var dispenserId = int.Parse(Console.ReadLine() ?? "0");
                    Console.WriteLine($"Enter Dispenser {i + 1} Product:");
                    var product = Console.ReadLine() ?? string.Empty;
                    innerBuilder.AddDispenser(dispenserId, product);
                }
            }
        };

        // Execute steps in random order
        while (steps.Count > 0)
        {
            var index = random.Next(1, steps.Count);
            steps[index - 1]();
            steps.RemoveAt(index - 1);
        }
    }
}