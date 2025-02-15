using Command;
using Command.Commands;

var pump = new Pump();
var client = new Client();
var rcc = new Rcc();

client.SelfService(pump, Pump.Product.Magna, 45);
Console.WriteLine("-----");

var dispenseFuelCommand = new DispenseFuelCommand(pump, Pump.Product.Premium, 30);
client.SetCommand(dispenseFuelCommand);
client.RequestFuelDispense();
Console.WriteLine("-----");

var cleanWindshieldCommand = new CleanWindshieldCommand(client, rcc, 1.5m);
client.SetCommand(cleanWindshieldCommand);
client.RequestCleanWindshield();