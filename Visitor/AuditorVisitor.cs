using Visitor.Pattern;

namespace Visitor;

public class AuditorVisitor : IVisitor
{
    public void Visit(OxxoGasStation oxxoGasStation)
    {
        var allowanceFuelSalesVolume = oxxoGasStation.AllowanceFuelSalesVolume;
        var fuelSalesVolume = oxxoGasStation.FuelSalesVolume;
        var autoJarVolume = oxxoGasStation.AutoJarVolume;
        var jarVolume = oxxoGasStation.JarVolume;
        var consignmentSalesVolume = oxxoGasStation.ConsignmentSalesVolume;
        var salesSelfConsumptionVolume = oxxoGasStation.SalesSelfConsumptionVolume;
        
        var totalReceptionsVolume = oxxoGasStation.TotalReceptionsVolume;
        var currentVolumeInTank = oxxoGasStation.CurrentVolumeInTank;
        
        Console.WriteLine("=== Auditing Oxxo Gas Station ===");
        Console.WriteLine($"Allowance Fuel Sales Volume: {allowanceFuelSalesVolume}");
        Console.WriteLine($"Fuel Sales Volume: {fuelSalesVolume}");
        Console.WriteLine($"Auto Jar Volume: {autoJarVolume}");
        Console.WriteLine($"Jar Volume: {jarVolume}");
        Console.WriteLine($"Consignment Sales Volume: {consignmentSalesVolume}");
        Console.WriteLine($"Sales Self Consumption Volume: {salesSelfConsumptionVolume}");
        
        var totalSales = fuelSalesVolume + autoJarVolume + jarVolume + consignmentSalesVolume + salesSelfConsumptionVolume;
        Console.WriteLine($"Total Sales: {totalSales}");
        
        var calculatedVolumeInTank = totalReceptionsVolume - totalSales;
        Console.WriteLine($"Calculated Volume in Tank: {calculatedVolumeInTank}");
        Console.WriteLine($"Current Volume in Tank: {currentVolumeInTank}");
        Console.WriteLine($"Difference: {currentVolumeInTank / totalReceptionsVolume * 100}%");
        
        Console.WriteLine("Publishing results to event hub...");
        Console.WriteLine("=== End of Audit ===...\n");
    }

    public void Visit(DealerGasStation dealerGasStation)
    {
        var fuelSalesVolume = dealerGasStation.FuelSalesVolume;
        var jarVolume = dealerGasStation.JarVolume;
        var salesSelfConsumptionVolume = dealerGasStation.SalesSelfConsumptionVolume;
        
        var totalReceptionsVolume = dealerGasStation.TotalReceptionsVolume;
        var currentVolumeInTank = dealerGasStation.CurrentVolumeInTank;
        
        Console.WriteLine("=== Auditing Dealer Gas Station ===");
        Console.WriteLine($"Fuel Sales Volume: {fuelSalesVolume}");
        Console.WriteLine($"Jar Volume: {jarVolume}");
        Console.WriteLine($"Sales Self Consumption Volume: {salesSelfConsumptionVolume}");
        
        var totalSales = fuelSalesVolume + jarVolume + salesSelfConsumptionVolume;
        Console.WriteLine($"Total Sales: {totalSales}");
        
        var calculatedVolumeInTank = totalReceptionsVolume - totalSales;
        Console.WriteLine($"Calculated Volume in Tank: {calculatedVolumeInTank}");
        Console.WriteLine($"Current Volume in Tank: {currentVolumeInTank}");
        Console.WriteLine($"Difference: {currentVolumeInTank / totalReceptionsVolume * 100}%");
        
        Console.WriteLine("Publishing results to private service...");
        Console.WriteLine("=== End of Audit ===...\n");
    }
}