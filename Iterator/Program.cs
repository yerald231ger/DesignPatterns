using Iterator;
using Iterator.Pattern;

var tanksList = new TankList();
var tankMagna = Tank.CreateDefaultTank("tank 1 magna", Product.Magna);
tankMagna.AddAlarmCode("A1");
tankMagna.AddAlarmCode("A2");
tankMagna.SetVolume(470);
tankMagna.SetTemperature(12);
tanksList.AddTank(tankMagna);

var tankPremium = Tank.CreateDefaultTank("tank 2 premium", Product.Premium);
tankPremium.SetVolume(384);
tankPremium.SetTemperature(23);
tankPremium.AddAlarmCode("A1");
tankPremium.AddAlarmCode("A2");
tankPremium.AddAlarmCode("A3");
tankPremium.AddAlarmCode("A4");
tanksList.AddTank(tankPremium);

var tankDiesel = Tank.CreateDefaultTank("tank 3 diesel", Product.Diesel);
tankDiesel.AddAlarmCode("A1");
tankDiesel.SetVolume(820);
tankDiesel.SetTemperature(35);
tanksList.AddTank(tankDiesel);

var tankMagna4 = Tank.CreateDefaultTank("tank 4 magna", Product.Magna);
tankMagna4.SetVolume(200);
tankMagna4.AddAlarmCode("A1");
tankMagna4.SetTemperature(-1);
tanksList.AddTank(tankMagna4);

var tanksByAlarm = tanksList.CreateMaxAlarmIterator();
var tanksByMinVolume = tanksList.CreateMinVolumeIterator();
var tanksByMaxTemperature = tanksList.CreateMaxTemperatureIterator();

foreach (var iterator in (IIterator[])[tanksByAlarm, tanksByMinVolume, tanksByMaxTemperature])
{
    Console.WriteLine($"Iterator: {iterator.GetType().Name}");
    foreach (var tank in iterator)
    {
        Console.WriteLine(tank);
    }
    Console.WriteLine();
}