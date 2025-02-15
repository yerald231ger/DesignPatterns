namespace Observer.Pattern;

public class EmailListener : IListener
{
    public void Update(Tank tank, string alarm)
    {
        Console.WriteLine($"New alarm for tank {tank.Code}: {alarm}. Sent by Email");
    }
}