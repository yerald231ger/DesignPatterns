using AbstractFactory.Pattern;

namespace AbstractFactory.Reporters;

public class TankReporter(int height, int width) : ITankReporter
{
    public void Write(int id, string product, decimal volume, decimal capacity)
    {
        Console.WriteLine("───────────────────────────────────────────────────────────────");
        Console.WriteLine();
        Console.WriteLine($"Tanque {id} of {product}".ToUpper());
        var fill = (int)(10m / capacity * volume);
        if (fill is < 1 or > 10)
        {
            Console.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        var fillHeight = (int)(height * (fill / 10.0)); // Compute how many rows to fill

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    switch (y)
                    {
                        case 0 when x == 0:
                            Console.Write('┌'); // Outline of the rectangle
                            break;
                        case 0 when x == width - 1:
                            Console.Write('┐'); // Outline of the rectangle
                            break;
                        default:
                        {
                            if (y == height - 1 && x == 0)
                                Console.Write('└'); // Outline of the rectangle
                            else if (y == height - 1 && x == width - 1)
                                Console.Write('┘'); // Outline of the rectangle
                            else if (y == 0 || y == height - 1)
                                Console.Write('─'); // Outline of the rectangle
                            else if (x == 0 || x == width - 1)
                                Console.Write('│'); // Outline of the rectangle
                            break;
                        }
                    }
                }
                else if (y >= height - fillHeight)
                {
                    Console.Write("█"); // Filling from bottom to top
                }
                else
                {
                    Console.Write(" "); // Empty space
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine($"Max Capacity: {capacity} liters");
        Console.WriteLine($"Current Volume: {volume} liters");
        Console.WriteLine();
    }
}

public class FileTankReporter(int height, int width) : ITankReporter
{
    public void Write(int id, string product, decimal volume, decimal capacity)
    {
        var fileName = $"tank_{id}.txt";
        using var writer = new StreamWriter(fileName);
        writer.WriteLine("───────────────────────────────────────────────────────────────");
        writer.WriteLine();
        writer.WriteLine($"Tanque {id} of {product}".ToUpper());
        var fill = (int)(10m / capacity * volume);
        if (fill is < 1 or > 10)
        {
            writer.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        var fillHeight = (int)(height * (fill / 10.0)); // Compute how many rows to fill

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    switch (y)
                    {
                        case 0 when x == 0:
                            writer.Write('┌'); // Outline of the rectangle
                            break;
                        case 0 when x == width - 1:
                            writer.Write('┐'); // Outline of the rectangle
                            break;
                        default:
                        {
                            if (y == height - 1 && x == 0)
                                writer.Write('└'); // Outline of the rectangle
                            else if (y == height - 1 && x == width - 1)
                                writer.Write('┘'); // Outline of the rectangle
                            else if (y == 0 || y == height - 1)
                                writer.Write('─'); // Outline of the rectangle
                            else if (x == 0 || x == width - 1)
                                writer.Write('│'); // Outline of the rectangle
                            break;
                        }
                    }
                }
                else if (y >= height - fillHeight)
                {
                    writer.Write("█"); // Filling from bottom to top
                }
                else
                {
                    writer.Write(" "); // Empty space
                }
            }

            writer.WriteLine();
        }
    }
}