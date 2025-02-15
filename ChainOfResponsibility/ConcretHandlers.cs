using ChainOfResponsibility.Pattern;

namespace ChainOfResponsibility;

public class StationSupplyTeam : BaseStationAlarmsHandler
{
    public override void Handle(StationAlarmsRequest request)
    {
        var supplyAlarms = request.Alarms.Extract(a => a.Contains("Low Product"));
        if (supplyAlarms.Count != 0)
        {
            foreach (var alarm in supplyAlarms)
                Console.WriteLine($"SupplyTeam: {alarm}");
        }

        base.Handle(request);
    }
}

public class MaintenanceTeam : BaseStationAlarmsHandler
{
    public override void Handle(StationAlarmsRequest request)
    {
        var maintenanceAlarms = request.Alarms.Extract(a => a.Contains("High Temperature"));
        if (maintenanceAlarms.Count != 0)
        {
            foreach (var alarm in maintenanceAlarms)
                Console.WriteLine($"MaintenanceTeam: {alarm}");
        }

        base.Handle(request);
    }
}

public class SupportTeamStationAlarmsHandler : BaseStationAlarmsHandler
{
    public override void Handle(StationAlarmsRequest request)
    {
        var supportAlarms = request.Alarms.Extract(a => a.Contains("Tcp Error"));
        if (supportAlarms.Count != 0)
        {
            foreach (var alarm in supportAlarms)
                Console.WriteLine($"SupportTeam: {alarm}");
        }

        base.Handle(request);
    }
}

public class DeveloperTeamStationAlarmsHandler : BaseStationAlarmsHandler
{
    public override void Handle(StationAlarmsRequest request)
    {
        var developerAlarms = request.Alarms.Extract(a => a.Contains("Memory Leak"));
        if (developerAlarms.Count != 0)
        {
            foreach (var alarm in developerAlarms)
                Console.WriteLine($"DeveloperTeam: {alarm}");
        }

        base.Handle(request);
    }
}