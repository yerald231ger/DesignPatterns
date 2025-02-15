namespace TemplateMethod.Pattern;

public class PineQuote(TableConfiguration tableConfiguration) : DefaultTemplateMethod(tableConfiguration)
{
    protected override string QuoteName => "for a Pine Table";

    protected override decimal CalculateChairs()
    {
        var totalChairs = TableConfiguration.GetNumberOfChairs();
        const decimal area = 100m;
        const decimal materialCost = 1.99m;
        var total = area * totalChairs * materialCost;
        Console.WriteLine($"Chairs cost: {total}");
        return total;
    }

    protected override decimal CalculateBenches()
    {
        var totalBenches = TableConfiguration.GetNumberOfBenches();
        const decimal area = 200m;
        const decimal materialCost = 2.20m;
        var total = area * totalBenches * materialCost;
        Console.WriteLine($"Benches cost: {total}");
        return total;
    }

    protected override decimal CalculateTable()
    {
        const decimal area = 750m;
        const decimal materialCost = 2.800m;
        const decimal total = area * materialCost;
        Console.WriteLine($"Table cost: {total}");
        return total;
    }

    protected override decimal ApplySpecialDiscount(decimal chairsCost, decimal benchesCost, decimal tableCost)
    {
        var total = base.ApplySpecialDiscount(chairsCost, benchesCost, tableCost);
        if (total <= 3000) return total;
        Console.WriteLine("Total discount applied");
        return total * 0.9m;
    }
}