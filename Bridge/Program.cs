using Bridge;

var horizontalTank = new Tank(TankFactory.GetTankType("Horizontal"));
var verticalTank = new Tank(TankFactory.GetTankType("Vertical"));

horizontalTank.SetFuelLevel(2);
verticalTank.SetFuelLevel(2);

horizontalTank.DisplayTankInfo();
verticalTank.DisplayTankInfo();