namespace TemplateMethod.Pattern;

public abstract class DefaultTemplateMethod(TableConfiguration tableConfiguration)
{
    protected readonly TableConfiguration TableConfiguration = tableConfiguration;
    protected abstract string QuoteName { get; }
    
    public void QuoteTable()
    {
        var chair = CalculateChairs();
        var bench = CalculateBenches();
        var table = CalculateTable();
        var total = ApplySpecialDiscount(chair, bench, table);
        Console.WriteLine($"The total price is {total} MXN. {QuoteName}");
        Console.WriteLine();
    }

    protected abstract decimal CalculateChairs();

    protected abstract decimal CalculateBenches();

    protected abstract decimal CalculateTable();

    protected virtual decimal ApplySpecialDiscount(decimal chairsCost, decimal benchesCost, decimal tableCost)
    {
        return chairsCost + benchesCost + tableCost;
    }
}