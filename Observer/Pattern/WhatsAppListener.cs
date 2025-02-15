namespace Observer.Pattern;

public class WhatsAppListener : IListener
{
    public void Update(Tank tank, string alarm)
    {
        Console.WriteLine($"New alarm for tank {tank.Code}: {alarm}. Sent by WhatsApp");
    }
}