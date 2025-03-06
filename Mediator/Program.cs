using Mediator;
using Mediator.Pattern;

Terminal.CreateTerminalDefaultTerminals();
var pump = new Pump();
var client = new Client("yerald231ger@gmail.com", "1234567890");

var rcc = new Rsc(pump, client);

client.AskForDispenseFuel(10m, Product.Premium);

Console.ReadLine();

