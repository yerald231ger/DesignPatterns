using Flywight.Pattern;

namespace Flywight;

public class FuelSaleFlyWeight
{
    public Guid Id { get; set; }
    public FlyWeight FlyWeight { get; set; }
    
    public static List<FuelSaleFlyWeight> GenerateSalesFlyWeight(int count)
    {
        var sales = new List<FuelSaleFlyWeight>();

        for (var i = 0; i < count; i++)
        {
            var sale = new FuelSaleFlyWeight
            {
                Id = Guid.NewGuid(),
                FlyWeight = FlyWeightFactory.GetFlyWeight(
                    ProductType.Create(Product.Diesel),
                    SaleType.Sale,
                    100,
                    2000,
                    1,
                    2
                )
            };
            
            sales.Add(sale);
        }

        return sales;
    }
}

public class FuelSale
{
    public Guid Id { get; set; }
    public ProductType ProductType { get; set; }
    public SaleType SaleType { get; set; }
    public decimal Volume { get; set; }
    public decimal Amount { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
        
    public static List<FuelSale> GenerateSales(int count)
    {
        var sales = new List<FuelSale>();

        for (var i = 0; i < count; i++)
        {
            var sale = new FuelSale
            {
                Id = Guid.NewGuid(),
                ProductType = ProductType.Create(Product.Diesel),
                SaleType = SaleType.Sale,
                Volume = 100,
                Amount = 2000,
                PumpNumber = 1,
                HoseNumber = 2
            };
            
            sales.Add(sale);
        }

        return sales;
    }
}

public record Nombre {}

public struct ProductType
{
    public Product Product { get; set; }
    public string Name { get; set; }
    
    public static ProductType Create(Product product)
    {
        return product switch
        {
            Product.Diesel => new ProductType { Product = Product.Diesel, Name = "Diesel" },
            Product.Magna => new ProductType { Product = Product.Magna, Name = "Magna" },
            Product.Premium => new ProductType { Product = Product.Premium, Name = "Premium" },
            _ => new ProductType { Product = Product.Diesel, Name = "Unknown" }
        };
    }
}

public enum Product
{
    Diesel,
    Magna,
    Premium
}

public enum SaleType
{
    Sale,
    Jar,
    AutoJar,
    Service
}