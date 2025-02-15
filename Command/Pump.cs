namespace Command;

public class Pump
{
    private List<Hose> _hoses = [];
    public decimal FinalPrice { get; private set; }

    public Pump()
    {
        _hoses.Add(new Hose { Product = Product.Magna, Price = 19.23m });
        _hoses.Add(new Hose { Product = Product.Premium, Price = 21.23m });
        _hoses.Add(new Hose { Product = Product.Diesel, Price = 22.23m });
    }

    public decimal DispenseFuel(Product product, decimal volume)
    {
        var hose = _hoses.First(hose => hose.Product == product);
        FinalPrice = hose.Price * volume;
        return volume;
    }

    public enum Product
    {
        Magna,
        Premium,
        Diesel
    }

    private class Hose
    {
        public required Product Product { get; init; }
        public required decimal Price { get; init; }
    }
}