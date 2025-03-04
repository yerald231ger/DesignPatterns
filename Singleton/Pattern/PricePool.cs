namespace Singleton.Pattern;

public sealed class PricePool
{
    private static readonly Lazy<PricePool> _instance = new(() => new PricePool());
    private readonly Dictionary<Product, List<decimal>> _priceHistory = new();

    private PricePool()
    {
        // Initialize price history with default prices
        SetPrice(Product.Magna, 18);
        SetPrice(Product.Premium, 20);
        SetPrice(Product.Diesel, 16);
    }

    public static PricePool Instance => _instance.Value;

    public void SetPrice(Product product, decimal price)
    {
        if (!_priceHistory.ContainsKey(product))
        {
            _priceHistory[product] = new List<decimal>();
        }

        // Add new price to history
        _priceHistory[product].Add(price);

        // Keep only the last 10 prices
        if (_priceHistory[product].Count > 10)
        {
            _priceHistory[product].RemoveAt(0);
        }
    }

    public decimal GetPrice(Product product)
    {
        return _priceHistory.TryGetValue(product, out var prices) && prices.Count > 0 
            ? prices.Last()  // Most recent price
            : 0;
    }

    public List<decimal> GetPriceHistory(Product product)
    {
        return _priceHistory.TryGetValue(product, out var prices) ? new List<decimal>(prices) : new List<decimal>();
    }

    public bool RemovePriceHistory(Product product)
    {
        return _priceHistory.Remove(product);
    }
}

public enum Product
{
    Premium,
    Magna,
    Diesel
}