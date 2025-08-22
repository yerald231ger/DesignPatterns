using Flywight.Pattern;

namespace Flywight;

public class FuelSaleWithFlyweight(FuelType fuelType, FuelSaleContext context)
{
    private readonly IFuelTypeFlyweight _fuelTypeFlyweight = FuelTypeFlyweightFactory.GetFuelTypeFlyweight(fuelType);

    public decimal CalculateAmount()
    {
        return _fuelTypeFlyweight.CalculateAmount(context.Volume, context.PricePerLiter);
    }

    public string GetDisplayInfo()
    {
        return _fuelTypeFlyweight.GetDisplayInfo(context);
    }

    public static List<FuelSaleWithFlyweight> GenerateSalesWithFlyweight(int count)
    {
        var sales = new List<FuelSaleWithFlyweight>();
        var random = new Random();
        var fuelTypes = Enum.GetValues<FuelType>();

        for (var i = 0; i < count; i++)
        {
            var fuelType = fuelTypes[random.Next(fuelTypes.Length)];
            var context = new FuelSaleContext
            {
                SaleId = Guid.NewGuid(),
                SaleDateTime = DateTime.Now.AddMinutes(-random.Next(1440)), // Last 24 hours
                PumpNumber = (short)random.Next(1, 13), // Pumps 1-12
                HoseNumber = (short)random.Next(1, 5), // Hoses 1-4
                Volume = Math.Round((decimal)(random.NextDouble() * 80 + 5), 2), // 5-85 liters
                PricePerLiter = fuelType switch
                {
                    FuelType.Premium => 24.50m,
                    FuelType.Magna => 22.30m,
                    FuelType.Diesel => 25.20m,
                    _ => 20.00m
                },
                CustomerCode = $"CUST{random.Next(1000, 9999)}"
            };

            sales.Add(new FuelSaleWithFlyweight(fuelType, context));
        }

        return sales;
    }
}

public class FuelSaleWithoutFlyweight
{
    public Guid SaleId { get; set; }
    public DateTime SaleDateTime { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
    public decimal Volume { get; set; }
    public decimal PricePerLiter { get; set; }
    public string CustomerCode { get; set; } = string.Empty;
    
    // Duplicated fuel type data (intrinsic state stored in each object)
    public string FuelName { get; set; } = string.Empty;
    public string FuelCode { get; set; } = string.Empty;
    public string FuelColor { get; set; } = string.Empty;

    public decimal CalculateAmount()
    {
        return Volume * PricePerLiter;
    }

    public string GetDisplayInfo()
    {
        return $"[{FuelCode}] {FuelName} ({FuelColor}) - " +
               $"Sale ID: {SaleId} | " +
               $"Pump: {PumpNumber} | " +
               $"Volume: {Volume:F2}L | " +
               $"Amount: ${CalculateAmount():F2}";
    }

    public static List<FuelSaleWithoutFlyweight> GenerateSalesWithoutFlyweight(int count)
    {
        var sales = new List<FuelSaleWithoutFlyweight>();
        var random = new Random();
        var fuelTypes = Enum.GetValues<FuelType>();

        for (var i = 0; i < count; i++)
        {
            var fuelType = fuelTypes[random.Next(fuelTypes.Length)];
            var (name, code, color) = fuelType switch
            {
                FuelType.Premium => ("Premium Gasoline", "PRM", "Gold"),
                FuelType.Magna => ("Magna Gasoline", "MAG", "Green"),
                FuelType.Diesel => ("Diesel Fuel", "DSL", "Blue"),
                _ => ("Unknown", "UNK", "Gray")
            };

            sales.Add(new FuelSaleWithoutFlyweight
            {
                SaleId = Guid.NewGuid(),
                SaleDateTime = DateTime.Now.AddMinutes(-random.Next(1440)),
                PumpNumber = (short)random.Next(1, 13),
                HoseNumber = (short)random.Next(1, 5),
                Volume = Math.Round((decimal)(random.NextDouble() * 80 + 5), 2),
                PricePerLiter = fuelType switch
                {
                    FuelType.Premium => 24.50m,
                    FuelType.Magna => 22.30m,
                    FuelType.Diesel => 25.20m,
                    _ => 20.00m
                },
                CustomerCode = $"CUST{random.Next(1000, 9999)}",
                FuelName = name,
                FuelCode = code,
                FuelColor = color
            });
        }

        return sales;
    }
}