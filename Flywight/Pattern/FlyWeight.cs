namespace Flywight.Pattern;

public class FlyWeight
{
    public ProductType ProductType { get; set; }
    public SaleType SaleType { get; set; }
    public decimal Volume { get; set; }
    public decimal Amount { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
}