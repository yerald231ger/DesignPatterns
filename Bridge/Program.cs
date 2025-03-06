using Bridge;

var horizontalTank = new Tank(TankFactory.GetTankType("Horizontal"));
var verticalTank = new Tank(TankFactory.GetTankType("Vertical"));

var tanks = new[] { horizontalTank, verticalTank };

foreach (var tank in tanks)
{
    tank.SetFuelLevel(2);
    tank.DisplayTankInfo();
    Console.WriteLine($"The tank type is {nameof(tank.TankType)}");
}