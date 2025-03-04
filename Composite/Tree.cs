using Composite.Pattern;

namespace Composite;

public class Dispenser(List<Pump> pumps) : IAmountCalculatorComposite
{
    public List<Pump> Pumps { get; } = pumps;

    public decimal GetAmount()
    {
        return Pumps.Sum(p => p.GetAmount());
    }

    public decimal GetAmount(Product productId)
    {
        return Pumps.Sum(p => p.GetAmount(productId));
    }
}

public class Pump(List<Hose> hoses) : IAmountCalculatorComposite
{
    public List<Hose> Hoses { get; } = hoses;

    public decimal GetAmount()
    {
        return Hoses.Sum(h => h.GetAmount());
    }

    public decimal GetAmount(Product productId)
    {
        return Hoses.Sum(h => h.GetAmount(productId));
    }
}

public class Hose(List<Sale> sales) : IAmountCalculatorComposite
{
    public List<Sale> Sales { get; } = sales;

    public decimal GetAmount()
    {
        return Sales.Sum(s => s.GetAmount());
    }

    public decimal GetAmount(Product productId)
    {
        return Sales.Where(s => s.ProductId == productId).Sum(s => s.GetAmount());
    }
}

public class Sale(Product productId, decimal volume) : IAmountCalculatorLeaf
{
    public Product ProductId { get; set; } = productId;
    public decimal Volume { get; set; } = volume;
    public decimal Amount { get; } = PricePool.GetPrice(productId) * volume;

    public decimal GetAmount()
    {
        return Amount;
    }
}

public abstract class PricePool
{
    public static decimal GetPrice(Product product)
    {
        return product switch
        {
            Product.Premium => 20,
            Product.Magna => 18,
            Product.Diesel => 16,
            _ => 0
        };
    }
}

public enum Product
{
    Premium = 1,
    Magna = 2,
    Diesel = 3
}