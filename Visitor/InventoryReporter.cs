using Visitor.Pattern;

namespace Visitor;

public class InventoryReporter : IVisitor
{
    public void Visit(OxxoGasStation oxxoGasStation)
    {
        Console.WriteLine("*** Oxxo Gas Station *** ");
        Console.WriteLine("Inventory: " + oxxoGasStation.CurrentVolumeInTank);
        Console.WriteLine("Odometer: " + oxxoGasStation.TotalDispensedVolume);
        
        Console.WriteLine("Sending report to Event Hub...\n");
    }

    public void Visit(DealerGasStation dealerGasStation)
    {
        Console.WriteLine("*** Dealer Gas Station ***");
        Console.WriteLine("Inventory: " + dealerGasStation.CurrentVolumeInTank);
        Console.WriteLine("Odometer: " + dealerGasStation.TotalDispensedVolume);
        
        Console.WriteLine("Sending report to Event dealer private service...\n");
    }
}