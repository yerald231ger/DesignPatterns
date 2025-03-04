using Composite;

var saleMagna = new Sale(Product.Magna, 10);
var salePremium = new Sale(Product.Premium, 10);
var saleDiesel = new Sale(Product.Diesel, 10);

var hosePremium = new Hose([salePremium]);
var hoseMagna = new Hose([saleMagna]);
var hoseDiesel = new Hose([saleDiesel]);

var pumpGasoline = new Pump([hosePremium, hoseMagna]);
var pumpDiesel = new Pump([hoseDiesel]);

var dispenser = new Dispenser([pumpGasoline, pumpDiesel]);

var totalSale = dispenser.GetAmount();
var totalSaleMagna = dispenser.GetAmount(Product.Magna);
var totalSalePremium = dispenser.GetAmount(Product.Premium);
var totalSalePumpGasoline = pumpGasoline.GetAmount();

Console.WriteLine("Total sale: " + totalSale);
Console.WriteLine("Total sale Magna: " + totalSaleMagna);
Console.WriteLine("Total sale Premium: " + totalSalePremium);
Console.WriteLine("Total sale Pump Magna: " + totalSalePumpGasoline);