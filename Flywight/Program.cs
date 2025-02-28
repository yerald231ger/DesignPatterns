using Flywight;

// var salesFw = FuelSaleFlyWeight.GenerateSalesFlyWeight(1_000_000);
var sales = FuelSale.GenerateSales(1_000_000_00);

// Console.WriteLine(salesFw.Count);
Console.WriteLine(sales.Count);
Console.ReadLine();