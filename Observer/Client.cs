namespace Observer;

public class Client(string name)
{
    public string Name { get; init; } = name;

    public void OnNewAlarm(Tank tank, string alarm)
    {
        Console.WriteLine($"New alarm for tank {tank.Code}: {alarm}. Client: {Name}");
    }
}