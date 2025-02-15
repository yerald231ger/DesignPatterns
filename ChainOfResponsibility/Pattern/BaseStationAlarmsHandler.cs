namespace ChainOfResponsibility.Pattern;

public abstract class BaseStationAlarmsHandler : IHandler<StationAlarmsRequest>
{
    private IHandler<StationAlarmsRequest>? _next;
    
    public virtual IHandler<StationAlarmsRequest> SetNext(IHandler<StationAlarmsRequest> handler)
    {
        _next = handler;
        return handler;
    }

    public virtual void Handle(StationAlarmsRequest request)
    {
        if (request.Alarms.Any())
            _next?.Handle(request);
    }
}

public class AlarmList : List<string>
{
    public AlarmList() { }
 
    public AlarmList(IEnumerable<string> collection) : base(collection) { }
    
    public AlarmList Extract(Predicate<string> match)
    {
        var extracted = FindAll(match);
        RemoveAll(match);
        return new AlarmList(extracted);
    }
}

public record StationAlarmsRequest(DateTime Time, Guid RequestId, string StationName, AlarmList Alarms);