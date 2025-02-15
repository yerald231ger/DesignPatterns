namespace Command;

public class Rcc
{
    public string Name { get; set; } = "RCC";

    public bool CleanWindshield(Client client, decimal tip)
    {
        client.CleanWindshield();
        Console.WriteLine($"windshield cleaned. tip: {tip}");
        return true;
    }
}