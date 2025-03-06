namespace FactoryMethod;

public interface IConsoleShapeWriter
{
    public void Write(int fill);
}

public class ConsoleShapeVerticalWriter(int height, int width) : IConsoleShapeWriter
{
    public void Write(int fill)
    {
        if (fill < 1 || fill > 10)
        {
            Console.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        int fillHeight = (int)(height * (fill / 10.0)); // Compute how many rows to fill

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    switch (y)
                    {
                        case 0 when x==0:
                            Console.Write('┌'); // Outline of the rectangle
                            break;
                        case 0 when x==width-1:
                            Console.Write('┐'); // Outline of the rectangle
                            break;
                        default:
                        {
                            if(y==height-1&& x==0)
                                Console.Write('└'); // Outline of the rectangle
                            else if(y==height-1&& x==width-1)
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
    }
}

public class ConsoleShapeHorizontalWriter(int height, int width) : IConsoleShapeWriter
{
    public void Write(int fill)
    {
        if (fill < 1 || fill > 10)
        {
            Console.WriteLine("Fill level must be between 1 and 10.");
            return;
        }

        int fillHeight = (int)(height * (fill / 10.0)); // Compute how many rows to fill

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (y == 0 || y == height - 1 || x == 0 || x == width - 1)
                {
                    switch (y)
                    {
                        case 0 when x==0:
                            Console.Write('┌'); // Outline of the rectangle
                            break;
                        case 0 when x==width-1:
                            Console.Write('┐'); // Outline of the rectangle
                            break;
                        default:
                        {
                            if(y==height-1&& x==0)
                                Console.Write('└'); // Outline of the rectangle
                            else if(y==height-1&& x==width-1)
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
    }
}

public class ConsoleShapeSphericalWriter(int radius) : IConsoleShapeWriter
{
    public void Write(int fill)
    {
        fill *= 2;
        double aspectRatio = 2.4; // Adjust for console character proportions
        int diameter = radius * 3;

        // Compute the threshold Y level where filling should start (from bottom)
        int fillThresholdY = (int)(radius - (radius * (fill / 10.0)));

        for (int y = -radius; y <= radius; y++)
        {
            for (int x = -diameter; x <= diameter; x++)
            {
                double distance = Math.Pow(x / aspectRatio, 2) + Math.Pow(y, 2);
                double outerThreshold = Math.Pow(radius + 0.5, 2);
                double innerThreshold = Math.Pow(radius - 0.5, 2);

                if (distance >= innerThreshold && distance <= outerThreshold)
                {
                    Console.Write("{"); // Circle outline
                }
                else if (distance < innerThreshold && y >= fillThresholdY)
                {
                    Console.Write("█"); // Fill progressively from bottom
                }
                else
                {
                    Console.Write(" "); // Empty space
                }
            }
            Console.WriteLine();
        }
    }
}

