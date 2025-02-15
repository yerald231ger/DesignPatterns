using Strategy.Pattern;

namespace Strategy;

public class Tank(Product product)
{
    public decimal Temperature { get; set; }

    public decimal Volume { get; set; }
    
    public decimal VolumeTc => _calculateTcStrategy.CalculateTc(this);

    public Product Product { get; private set; } = product;
    private ICalculateTcStrategy _calculateTcStrategy = GetCalculateTcStrategy(product);
    
    public void ChangeProduct(Product product)
    {
        Product = product;
        _calculateTcStrategy = GetCalculateTcStrategy(product);
    }

    private static ICalculateTcStrategy GetCalculateTcStrategy(Product product)
        => product switch
        {
            Product.Magna or Product.Premium => new DieselCalculateTcStrategy(),
            Product.Diesel => new GasolineCalculateTcStrategy(),
            _ => throw new ArgumentOutOfRangeException(nameof(product), product, null)
        };

    private class DieselCalculateTcStrategy : ICalculateTcStrategy
    {
        const int _temperatureCompensation = 20;
        const decimal _coefficient = 0.000096m;

        public decimal CalculateTc(Tank tank)
        {
            var volume = tank.Volume;
            var temperature = tank.Temperature;
            var tc = volume * (1 - _coefficient * (temperature - _temperatureCompensation));
            return tc;
        }
    }

    private class GasolineCalculateTcStrategy : ICalculateTcStrategy
    {
        const int _temperatureCompensation = 12;
        const decimal _coefficient = 0.000089m;

        public decimal CalculateTc(Tank tank)
        {
            var volume = tank.Volume;
            var temperature = tank.Temperature;
            var tc = volume * (1 - _coefficient * (temperature - _temperatureCompensation));
            return tc;
        }
    }
}