// See https://aka.ms/new-console-template for more information

using ChainOfResponsibility;
using ChainOfResponsibility.Pattern;


BaseStationAlarmsHandler stationSupplyTeam = new StationSupplyTeam();
var supportTeamHandler = new SupportTeamStationAlarmsHandler();
var maintenanceTeam = new MaintenanceTeam();
var developerTeamHandler = new DeveloperTeamStationAlarmsHandler();

var alarms = new AlarmList { "Other" };
var request = new StationAlarmsRequest(DateTime.Now, Guid.NewGuid(), "Station 1", alarms);

stationSupplyTeam
    .SetNext(maintenanceTeam)
    .SetNext(supportTeamHandler)
    .SetNext(developerTeamHandler);

stationSupplyTeam.Handle(request);


