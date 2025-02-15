namespace Observer.Pattern;

public interface IListener
{
    public void Update(Tank tank, string alarm);
}