namespace TemplateMethod.Pattern;

public class BasicQuote(TableConfiguration tableConfiguration) : DefaultTemplateMethod(tableConfiguration)
{
    protected override string QuoteName { get; } = "table with basic materials";
    protected override decimal CalculateChairs()
    {
        var totalChairs = TableConfiguration.GetNumberOfChairs();
        const decimal area = 90m;
        const decimal materialCost = 2.25m;
        var total = area * totalChairs * materialCost;
        Console.WriteLine($"Chairs cost: {total}");
        return total;
    }

    protected override decimal CalculateBenches()
    {
        var totalBenches = TableConfiguration.GetNumberOfBenches();
        const decimal area = 200m;
        const decimal materialCost = 2.00m;
        var total = area * totalBenches * materialCost;
        Console.WriteLine($"Benches cost: {total}");
        return total;
    }

    protected override decimal CalculateTable()
    {
        const decimal area = 650m;
        const decimal materialCost = 2.20m;
        const decimal total = area * materialCost;
        Console.WriteLine($"Table cost: {total}");
        return total;
    }
}