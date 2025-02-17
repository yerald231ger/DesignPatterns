
using Decorator;
using Decorator.Pattern;

IGasStationService gasStationService = new FuelDispense
{
    Product = FuelDispense.ProductType.Premium,
    Volume = 35
};

Console.WriteLine($"Cost of fuel dispense service: {gasStationService.GetServiceCost()}");

gasStationService = new GasolineAdditiveDecorator(gasStationService);
gasStationService = new FilterChangeDecorator(gasStationService);

Console.WriteLine($"Cost of fuel dispense service: {gasStationService.GetServiceCost()}");
