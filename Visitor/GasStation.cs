using Visitor.Pattern;

namespace Visitor;

public class OxxoGasStation : IServiceStation
{ 
    public decimal AllowanceFuelSalesVolume { get; set; }
    public decimal FuelSalesVolume { get; set; }
    public decimal AutoJarVolume { get; set; }
    public decimal JarVolume { get; set; }
    public decimal ConsignmentSalesVolume { get; set; }
    public decimal SalesSelfConsumptionVolume { get; set; }
    
    public decimal TotalReceptionsVolume { get; set; }
    public decimal CurrentVolumeInTank { get; set; }
    public decimal TotalDispensedVolume { get; set; }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public static OxxoGasStation Create()
    {
        return new OxxoGasStation
        {
            AllowanceFuelSalesVolume = 600,
            FuelSalesVolume = 5200,
            AutoJarVolume = 5,
            JarVolume = 3,
            ConsignmentSalesVolume = 500,
            SalesSelfConsumptionVolume = 2300,
            TotalReceptionsVolume = 6000,
            CurrentVolumeInTank = 1000,
            TotalDispensedVolume = 5000
        };
    }
}

public class DealerGasStation : IServiceStation
{
    public decimal FuelSalesVolume { get; set; }
    public decimal JarVolume { get; set; }
    public decimal SalesSelfConsumptionVolume { get; set; }
    
    public decimal TotalReceptionsVolume { get; set; }
    public decimal CurrentVolumeInTank { get; set; }
    public decimal TotalDispensedVolume { get; set; }
    
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
    
    public static DealerGasStation Create()
    {
        return new DealerGasStation
        {
            FuelSalesVolume = 5200,
            JarVolume = 3,
            SalesSelfConsumptionVolume = 2300,
            TotalReceptionsVolume = 6000,
            CurrentVolumeInTank = 1000,
            TotalDispensedVolume = 5000
        };
    }
}