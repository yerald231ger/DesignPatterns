namespace Bridge.Pattern;

public interface ITankRenderer
{
    void RenderTank(string tankId, string product, decimal volume, decimal capacity);
    void RenderFuelLevel(int level, decimal percentage);
}

public class ConsoleTankRenderer : ITankRenderer
{
    public void RenderTank(string tankId, string product, decimal volume, decimal capacity)
    {
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine($"TANK {tankId} - {product.ToUpper()}");
        Console.WriteLine("═══════════════════════════════════════");
        Console.WriteLine($"Volume: {volume:F2} L / {capacity:F2} L");
        
        var percentage = (volume / capacity) * 100;
        RenderFuelLevel((int)(percentage / 10), percentage);
        Console.WriteLine();
    }

    public void RenderFuelLevel(int level, decimal percentage)
    {
        Console.Write("Level: [");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(i < level ? "█" : "░");
        }
        Console.WriteLine($"] {percentage:F1}%");
    }
}

public class FileTankRenderer : ITankRenderer
{
    private readonly string _outputDirectory;

    public FileTankRenderer(string outputDirectory = "tank_reports")
    {
        _outputDirectory = outputDirectory;
        Directory.CreateDirectory(_outputDirectory);
    }

    public void RenderTank(string tankId, string product, decimal volume, decimal capacity)
    {
        var fileName = Path.Combine(_outputDirectory, $"tank_{tankId}.txt");
        using var writer = new StreamWriter(fileName);
        
        writer.WriteLine("═══════════════════════════════════════");
        writer.WriteLine($"TANK {tankId} - {product.ToUpper()}");
        writer.WriteLine("═══════════════════════════════════════");
        writer.WriteLine($"Volume: {volume:F2} L / {capacity:F2} L");
        
        var percentage = (volume / capacity) * 100;
        writer.Write("Level: [");
        var level = (int)(percentage / 10);
        for (int i = 0; i < 10; i++)
        {
            writer.Write(i < level ? "█" : "░");
        }
        writer.WriteLine($"] {percentage:F1}%");
        writer.WriteLine($"Report generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
    }

    public void RenderFuelLevel(int level, decimal percentage)
    {
        // Implementation for standalone fuel level rendering if needed
    }
}

public class JsonTankRenderer : ITankRenderer
{
    private readonly string _outputDirectory;

    public JsonTankRenderer(string outputDirectory = "tank_json")
    {
        _outputDirectory = outputDirectory;
        Directory.CreateDirectory(_outputDirectory);
    }

    public void RenderTank(string tankId, string product, decimal volume, decimal capacity)
    {
        var fileName = Path.Combine(_outputDirectory, $"tank_{tankId}.json");
        var percentage = (volume / capacity) * 100;
        var level = (int)(percentage / 10);
        
        var tankData = new
        {
            TankId = tankId,
            Product = product,
            Volume = volume,
            Capacity = capacity,
            Percentage = Math.Round(percentage, 1),
            Level = level,
            Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        };
        
        var json = System.Text.Json.JsonSerializer.Serialize(tankData, new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(fileName, json);
    }

    public void RenderFuelLevel(int level, decimal percentage)
    {
        // Implementation for standalone fuel level rendering if needed
    }
}