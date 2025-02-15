using Observer.Pattern;

namespace Observer;

public delegate void AlarmEventHandler(Tank tank, string alarm);

public class Tank : IPublisher
{
    private readonly List<string> _alarms = [];
    private readonly List<IListener> _listeners = [];

    public required string Code { get; init; }
    public event AlarmEventHandler AlarmEvent = delegate { };

    public void AddAlarm(string alarm)
    {
        _alarms.Add(alarm);
        AlarmEvent(this, alarm);
        Notify(alarm);
    }

    public void Subscribe(IListener listener)
        => _listeners.Add(listener);


    public void Unsubscribe(IListener listener)
        => _listeners.Remove(listener);

    private void Notify(string alarm) 
        => _listeners.ForEach(listener => listener.Update(this, alarm));
}