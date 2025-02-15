namespace Mediator.Pattern;

public interface IMediator
{
    public void Notify<T>(object sender, string @event, T data);
    public void AsignSender<T>(T sender);
}