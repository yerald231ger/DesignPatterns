using System.Text.Json;
using Memento;
using Memento.Pattern;

var caretaker = new Caretaker("Station 1");

var tank1 = new Tank { Code = "T1", Volume = 1000, Product = Product.Magna };
var tank2 = new Tank { Code = "T2", Volume = 2000, Product = Product.Premium };

var hose1 = new Hose("H1");
var hose2 = new Hose("H2");
var hose3 = new Hose("H3");
var hose4 = new Hose("H4");

var pump1 = new Pump("P1");
var pump2 = new Pump("P2");

var dispenser1 = new Dispenser("D1");

caretaker.SetTankToHose(hose1, tank1);
caretaker.SetTankToHose(hose2, tank2);

caretaker.SetTankToHose(hose3, tank1);
caretaker.SetTankToHose(hose4, tank2);

caretaker.SetHoseToPump(pump1, hose1);
caretaker.SetHoseToPump(pump1, hose2);

caretaker.SetHoseToPump(pump2, hose3);
caretaker.SetHoseToPump(pump2, hose4);

caretaker.SetPumpToDispenser(dispenser1, pump1);
caretaker.SetPumpToDispenser(dispenser1, pump2);

caretaker.AddTank(tank1);
caretaker.AddTank(tank2);
caretaker.AddDispenser(dispenser1);

var station = caretaker.GetStation();

Console.WriteLine(JsonSerializer.Serialize(station));

caretaker.RemoveTank(tank2);

var station2 = caretaker.GetStation();

Console.WriteLine(JsonSerializer.Serialize(station2));

caretaker.Undo();

var station3 = caretaker.GetStation();

Console.WriteLine(JsonSerializer.Serialize(station3));