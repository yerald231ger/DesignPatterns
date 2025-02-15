namespace Observer.Pattern;

public interface IPublisher
{
    public void Subscribe(IListener listener);
    public void Unsubscribe(IListener listener);
}