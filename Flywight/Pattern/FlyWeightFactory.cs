namespace Flywight.Pattern;

public class FlyWeightFactory
{
    private static readonly Dictionary<string, FlyWeight> _flyweights = new Dictionary<string, FlyWeight>();

    public static FlyWeight GetFlyWeight(ProductType productType, SaleType saleType, decimal volume, decimal amount, short pumpNumber, short hoseNumber)
    {
        var key = $"{productType.Name}-{saleType}-{volume}-{amount}-{pumpNumber}-{hoseNumber}";

        if (_flyweights.ContainsKey(key))
        {
            return _flyweights[key];
        }

        var flyweight = new FlyWeight
        {
            ProductType = productType,
            SaleType = saleType,
            Volume = volume,
            Amount = amount,
            PumpNumber = pumpNumber,
            HoseNumber = hoseNumber
        };

        _flyweights.Add(key, flyweight);
        return flyweight;
    }
}