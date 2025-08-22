namespace Flywight.Pattern;

public interface IFuelTypeFlyweight
{
    decimal CalculateAmount(decimal volume, decimal pricePerLiter);
    string GetDisplayInfo(FuelSaleContext context);
}

public class FuelTypeFlyweight : IFuelTypeFlyweight
{
    private readonly string _fuelName;
    private readonly string _fuelCode;
    private readonly string _color;

    public FuelTypeFlyweight(string fuelName, string fuelCode, string color)
    {
        _fuelName = fuelName;
        _fuelCode = fuelCode;
        _color = color;
    }

    public decimal CalculateAmount(decimal volume, decimal pricePerLiter)
    {
        return volume * pricePerLiter;
    }

    public string GetDisplayInfo(FuelSaleContext context)
    {
        return $"[{_fuelCode}] {_fuelName} ({_color}) - " +
               $"Sale ID: {context.SaleId} | " +
               $"Pump: {context.PumpNumber} | " +
               $"Volume: {context.Volume:F2}L | " +
               $"Amount: ${CalculateAmount(context.Volume, context.PricePerLiter):F2}";
    }
}

public class FuelSaleContext
{
    public Guid SaleId { get; set; }
    public DateTime SaleDateTime { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
    public decimal Volume { get; set; }
    public decimal PricePerLiter { get; set; }
    public string CustomerCode { get; set; } = string.Empty;
}