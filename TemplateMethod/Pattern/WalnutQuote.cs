namespace TemplateMethod.Pattern;

public class WalnutQuote(TableConfiguration tableConfiguration) : DefaultTemplateMethod(tableConfiguration)
{
    protected override string QuoteName => "for a Walnut Table";

    protected override decimal CalculateChairs()
    {
        var totalChairs = TableConfiguration.GetNumberOfChairs();
        const decimal area = 110m;
        const decimal materialCost = 6.525m;
        var total = area * totalChairs * materialCost;
        Console.WriteLine($"Chairs cost: {total}");
        return total;
    }

    protected override decimal CalculateBenches()
    {
        var totalBenches = TableConfiguration.GetNumberOfBenches();
        const decimal area = 200m;
        const decimal materialCost = 8.500m;
        var total = area * totalBenches * materialCost;
        Console.WriteLine($"Benches cost: {total}");
        return total;
    }

    protected override decimal CalculateTable()
    {
        var area = TableConfiguration.GetAreaTable();
        const decimal materialCost = 10.500m;
        var total = (decimal)area * materialCost;

        switch (area)
        {
            case >= 1200:
                total *= 0.85m;
                Console.WriteLine("Discount applied for walnut table");
                break;
            case >= 1000:
                total *= 0.9m;
                Console.WriteLine("Discount applied for walnut table");
                break;
            default:
                Console.WriteLine("If you choose a table with an area major than 1000, you will have a discount of 10%");
                break;
        }

        Console.WriteLine($"Table cost: {total}");
        return total;
    }
}