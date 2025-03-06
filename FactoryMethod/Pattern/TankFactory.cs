namespace FactoryMethod.Pattern;

public abstract class TankFactory
{
    protected abstract Tank CreateTank(int tankId, Product product);
    
    public void SomeOperation(string tankId, string productId, int level)
    {
        var id = int.Parse(tankId);
        var product = productId switch {
            "Magna" => new Product(),
            "Premium" => new Product(Product.ProductType.Premium, 0.00098m),
            "Diesel" => new Product(Product.ProductType.Diesel, 0.00086m),
            "Urea" => new Product(Product.ProductType.Urea, 0.00102m),
            _ => throw new NotSupportedException()
        };
        var tank = CreateTank(id, product);
        tank.SetCurrentFuelLevel(level);
        tank.WriteToConsole();
    }
}

public class HorizontalTankFactory : TankFactory
{
    protected override Tank CreateTank(int tankId, Product product)
    {
        return new HorizontalTank { TankId = tankId, Product = product };
    }
}

public class VerticalTankFactory : TankFactory
{
    protected override Tank CreateTank(int tankId, Product product)
    {
        return new VerticalTank { TankId = tankId, Product = product };
    }
}

public class SphericalTankFactory : TankFactory
{
    protected override Tank CreateTank(int tankId, Product product)
    {
        return new SphericalTank { TankId = tankId, Product = product };
    }
}