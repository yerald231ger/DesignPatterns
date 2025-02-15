namespace TemplateMethod.Pattern;

public class CherryDynamicQuote(TableConfiguration tableConfiguration) : DefaultTemplateMethod(tableConfiguration)
{
    private static decimal GetDynamicCost()
    {
        const double currentReference = 2.3d;
        var dynamic = Random.Shared.NextDouble();
        var result = currentReference + dynamic;
        return (decimal)result;
    }

    protected override string QuoteName => "for a cherry table";

    protected override decimal CalculateChairs()
    {
        var totalChairs = TableConfiguration.GetNumberOfChairs();
        const decimal area = 100m;
        var materialCost = GetDynamicCost();
        var total = area * totalChairs * materialCost;
        Console.WriteLine($"Chairs with dynamic cost: {total}");
        return total;
    }

    protected override decimal CalculateBenches()
    {
        var totalBenches = TableConfiguration.GetNumberOfBenches();
        const decimal area = 200m;  
        var materialCost = GetDynamicCost();
        var total = area * totalBenches * materialCost;
        Console.WriteLine($"Benches with dynamic cost: {total}");
        return total;
    }

    protected override decimal CalculateTable()
    {
        var area = TableConfiguration.GetAreaTable();
        var materialCost = GetDynamicCost();
        var total = (decimal)area * materialCost;
        Console.WriteLine($"Table with dynamic cost: {total}");
        return total;
    }
}