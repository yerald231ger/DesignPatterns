using Decorator.Pattern;

namespace Decorator;

public class FuelDispense : IGasStationService
{
    public ProductType Product { get; set; }
    public decimal Volume { get; set; }
    
    public decimal GetServiceCost()
    {
        var totalCost = Product switch
        {
            ProductType.Premium => 18.23m * Volume,
            ProductType.Magna => 21.12m * Volume,
            ProductType.Diesel => 23.56m * Volume,
            _ => 0
        };
        
        return totalCost;
    }
    
    public enum ProductType
    {
        Premium,
        Magna,
        Diesel
    }
}

