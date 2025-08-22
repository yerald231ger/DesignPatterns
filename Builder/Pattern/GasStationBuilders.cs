using Builder.Models;

namespace Builder.Pattern;

public class TankBuilder
{
    private short TankId { get; set; }
    private ProductId ProductId { get; set; }
    private Product? Product { get; set; }
    private string Code { get; set; } = string.Empty;
    private string Description { get; set; } = string.Empty;
    private decimal UtilCapacity { get; set; }
    private decimal OperatingCapacity { get; set; }
    private decimal FondageCapacity { get; set; }
    private decimal TotalStorageCapacity { get; set; }
    private decimal MinimumOperatingVolume { get; set; }
    private DateTime CalibrationDate { get; set; }
    private bool IsActive { get; set; }
    private TankReading? FirstTankReading { get; set; }
    private TankReading? LastTankReading { get; set; }

    public TankBuilder Default(Func<StationBuilder.DefaultProducts, Product> selector)
    {
        Product = selector(StationBuilder.ProductSelector);

        switch (Product.ProductName)
        {
            case "Magna":
                TankId = 1;
                ProductId = Product.ProductId;
                Code = "STQ-EDS-0001";
                Description = "Tanque 1 - Magna";
                UtilCapacity = 57000.000m;
                OperatingCapacity = 51300.000m;
                FondageCapacity = 8550.000m;
                TotalStorageCapacity = 54150.000m;
                MinimumOperatingVolume = 8550.000m;
                CalibrationDate = new DateTime(2025, 3, 8, 10, 36, 45, 327);
                IsActive = true;
                break;
            case "Premium":
                TankId = 2;
                ProductId = Product.ProductId;
                Code = "STQ-EDS-0002";
                Description = "Tanque 2 - Premium";
                UtilCapacity = 57000.000m;
                OperatingCapacity = 51300.000m;
                FondageCapacity = 8550.000m;
                TotalStorageCapacity = 54150.000m;
                MinimumOperatingVolume = 8550.000m;
                CalibrationDate = new DateTime(2025, 3, 8, 10, 36, 45, 327);
                IsActive = true;
                break;
            case "Diésel":
                TankId = 3;
                ProductId = Product.ProductId;
                Code = "STQ-EDS-0003";
                Description = "Tanque 3 - Diésel";
                UtilCapacity = 95000.000m;
                OperatingCapacity = 85500.000m;
                FondageCapacity = 14250.000m;
                TotalStorageCapacity = 90250.000m;
                MinimumOperatingVolume = 14250.000m;
                CalibrationDate = new DateTime(2025, 3, 8, 10, 36, 45, 327);
                IsActive = true;
                break;
        }

        return this;
    }

    public TankBuilder WithNumber(short number)
    {
        TankId = number;
        Code = $"STQ-EDS-{number:0000}";
        Description = $"Tanque {number} - Premium";
        return this;
    }

    public TankBuilder WithUtilCapacity(decimal utilCapacity)
    {
        UtilCapacity = utilCapacity;
        return this;
    }

    public TankBuilder WithFirstAndLastTankReading(int initialVolume, int finalVolume)
    {
        if(Product == null)
            throw new InvalidOperationException("Product must be set before adding tank readings.");
        
        FirstTankReading = new TankReading
        {
            TankId = TankId,
            ReadingId = 783001,
            CurrentVolume = initialVolume,
            CurrentTcVolume = initialVolume,
            Temperature = 25.0m,
            ReadingDate = DateTime.Now.AddHours(-24),
            IntegrityHash = null,
            ProductId = ProductId,
            ProductName = Product!.ProductName
        };
        
        LastTankReading = new TankReading
        {
            TankId = TankId,
            ReadingId = 783002,
            CurrentVolume = finalVolume,
            CurrentTcVolume = finalVolume,
            Temperature = 25.0m,
            ReadingDate = DateTime.Now,
            IntegrityHash = null,
            ProductId = ProductId,
            ProductName = Product!.ProductName
        };
        
        return this;
    }

    public Tank Build() =>
        new Tank
        {
            TankId = TankId,
            ProductId = ProductId,
            Product = Product,
            Code = Code,
            Description = Description,
            UtilCapacity = UtilCapacity,
            OperatingCapacity = OperatingCapacity,
            FondageCapacity = FondageCapacity,
            TotalStorageCapacity = TotalStorageCapacity,
            MinimumOperatingVolume = MinimumOperatingVolume,
            CalibrationDate = CalibrationDate,
            IsActive = IsActive,
            FirstTankReading = FirstTankReading,
            LastTankReading = LastTankReading
        };
}

public class DispenserBuilder
{
    private Guid Id { get; set; }
    private short DispenserId { get; set; }
    private string MeasurementSystem { get; set; } = string.Empty;
    private string Code { get; set; } = string.Empty;
    private string Description { get; set; } = string.Empty;
    private decimal MeasurementUncertainty { get; set; }
    private DateTime CalibrationValidity { get; set; }
    private bool IsActive { get; set; }
    private DateTime LastCalibrationDate { get; set; }
    private short ValidityTime { get; set; }
    private List<Pump> Pumps { get; set; } = [];
    public static short DispenserNumberCounter { get; set; } = 1;
    private readonly PumpBuilder _pumpBuilder = new();
    private readonly List<short> _dispenserIds = [];

    public DispenserBuilder Default()
    {
        Id = Guid.NewGuid();
        MeasurementUncertainty = 0.05m;
        CalibrationValidity = new DateTime(2025, 3, 8, 10, 36, 54, 833);
        IsActive = true;
        LastCalibrationDate = new DateTime(2025, 3, 8, 10, 36, 54, 833);
        ValidityTime = 1;
        return this;
    }

    public DispenserBuilder WithNumber(short number)
    {
        if (_dispenserIds.Contains(number))
            throw new ArgumentException($"The dispenser number {number} is already in use.");

        DispenserId = DispenserNumberCounter;
        MeasurementSystem = DispenserNumberCounter.ToString("0000");
        Code = DispenserNumberCounter.ToString("0000");
        Description = $"Dispensador {DispenserNumberCounter:0000}";
        return this;
    }

    public DispenserBuilder AddPump(Action<PumpBuilder> builder)
    {
        _pumpBuilder.CreateDefault();
        builder(_pumpBuilder);
        Pumps.Add(_pumpBuilder.Build());
        return this;
    }

    public Dispenser Build()
    {
        return new Dispenser
        {
            Id = Id,
            DispenserId = DispenserId,
            MeasurementSystem = MeasurementSystem,
            Code = Code,
            Description = Description,
            MeasurementUncertainty = MeasurementUncertainty,
            CalibrationValidity = CalibrationValidity,
            IsActive = IsActive,
            LastCalibrationDate = LastCalibrationDate,
            ValidityTime = ValidityTime,
            Pumps = Pumps
        };
    }
}

public class PumpBuilder
{
    private Guid Id { get; set; }
    private short PumpId { get; set; }
    private short PumpNumber { get; set; }
    private string Description { get; set; } = string.Empty;
    private bool IsActive { get; set; }
    private List<Hose> Hoses { get; set; } = [];
    private readonly HoseBuilder _hoseBuilder = new();
    private readonly List<short> _pumpIds = [];

    public PumpBuilder CreateDefault()
    {
        Id = Guid.NewGuid();
        IsActive = true;
        return this;
    }

    public PumpBuilder WithNumber(short number)
    {
        if (_pumpIds.Contains(number))
            throw new ArgumentException($"The pump number {number} is already in use.");

        PumpId = number;
        PumpNumber = number;
        Description = $"Bomba {number:000}";
        return this;
    }

    public PumpBuilder AddHose(Action<HoseBuilder> hoseBuilder)
    {
        _hoseBuilder.CreateDefault();
        hoseBuilder(_hoseBuilder);
        Hoses.Add(_hoseBuilder.Build());
        return this;
    }

    public Pump Build()
    {
        return new Pump
        {
            Id = Id,
            PumpId = PumpId,
            PumpNumber = PumpNumber,
            Description = Description,
            IsActive = IsActive,
            Hoses = Hoses
        };
    }
}

public class HoseBuilder
{
    private Guid Id { get; set; }
    private short HoseNumber { get; set; }
    private short? TankId { get; set; }
    private Tank? Tank { get; set; }
    private short? ProductId { get; set; }
    private Product? Product { get; set; }
    private List<DeliveryInventory> DeliveryInventories { get; set; } = [];

    public HoseBuilder CreateDefault()
    {
        Id = Guid.NewGuid();
        DeliveryInventories = [];
        return this;
    }

    public HoseBuilder WithNumber(short number)
    {
        HoseNumber = number;
        return this;
    }

    public HoseBuilder WithTankSuction(Tank tank)
    {
        Tank = tank;
        TankId = tank.TankId;
        ProductId = tank.ProductId;
        Product = tank.Product;
        return this;
    }

    public Hose Build()
    {
        return new Hose
        {
            Id = Id,
            HoseNumber = HoseNumber,
            TankId = TankId ?? 0,
            Tank = Tank,
            ProductId = ProductId ?? 0,
            Product = Product,
            DeliveryInventories = DeliveryInventories
        };
    }
}

public class StationBuilder
{
    public class DefaultProducts
    {
        public readonly Product ProductMagna = CreateMagnaProduct();
        public readonly Product ProductPremium = CreatePremiumProduct();
        public readonly Product ProductDiesel = CreateDieselProduct();
    }

    public static DefaultProducts ProductSelector { get; } = new();
    private List<Tank> _tanks;
    private List<Dispenser> _dispensers;

    public StationBuilder CreateDefault()
    {
        var tankBuilder = new TankBuilder();

        var magnaTank = tankBuilder
            .Default(p => p.ProductMagna)
            .WithNumber(1)
            .WithFirstAndLastTankReading(10000, 70000)
            .Build();

        var premiumTank = tankBuilder
            .Default(p => p.ProductPremium)
            .WithNumber(2)
            .WithFirstAndLastTankReading(10000, 70000)
            .Build();

        var dieselTank = tankBuilder
            .Default(p => p.ProductDiesel)
            .WithNumber(3)
            .WithFirstAndLastTankReading(10000, 80000)
            .Build();

        var gasolineDispenserOne = new DispenserBuilder()
            .Default()
            .WithNumber(1)
            .AddPump(pb =>
                pb.WithNumber(1)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                    .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
            )
            .AddPump(pb =>
                pb.WithNumber(2)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                    .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
            ).Build();

        var gasolineDispenserTwo = new DispenserBuilder()
            .Default()
            .WithNumber(2)
            .AddPump(pb =>
                pb.WithNumber(3)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                    .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
            )
            .AddPump(pb =>
                pb.WithNumber(4)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(magnaTank))
                    .AddHose(hb => hb.WithNumber(2).WithTankSuction(premiumTank))
            ).Build();

        var dieselDispenserThree = new DispenserBuilder()
            .Default()
            .WithNumber(3)
            .AddPump(pb =>
                pb.WithNumber(5)
                    .AddHose(hb => hb.WithNumber(1).WithTankSuction(dieselTank))
            ).Build();

        _tanks = [magnaTank, premiumTank, dieselTank];
        _dispensers = [gasolineDispenserOne, gasolineDispenserTwo, dieselDispenserThree];
        return this;
    }

    public StationBuilder LoadReceptionInventories(ReceptionInventory[] receptionInventories)
    {
        foreach (var tank in _tanks)
        {
            tank.ReceptionInventories = receptionInventories
                .Where(ri => ri.TankId == tank.TankId)
                .ToList();
        }

        return this;
    }

    public StationBuilder LoadDeliveryInventories(DetailedDeliveryInventory[] deliveryInventories)
    {
        foreach (var dispenser in _dispensers)
        {
            foreach (var pump in dispenser.Pumps)
            {
                foreach (var hose in pump.Hoses)
                {
                    hose.DeliveryInventories = deliveryInventories
                        .Where(di => di.PumpNumber == pump.PumpNumber && di.HoseNumber == hose.HoseNumber)
                        .Select(ddi => new DeliveryInventory
                        {
                            DeliveryInventoryId = ddi.DeliveryInventoryId,
                            DeliveredAmount = ddi.DeliveredAmount,
                            PumpNumber = ddi.PumpNumber,
                            HoseNumber = ddi.HoseNumber,
                            SaleTypeId = ddi.SaleTypeId,
                            DeliveredVolume = ddi.DeliveredVolume,
                            InterfaceTransactionDateTime = ddi.InterfaceTransactionDateTime
                        })
                        .ToList();
                }
            }
        }

        return this;
    }

    public Station Build()
    {
        return new Station
        {
            Tanks = _tanks,
            Dispensers = _dispensers
        };
    }

    private static Product CreateMagnaProduct()
    {
        return new Product
        {
            ProductId = ProductId.Create(2),
            ProductNo = 2,
            ProductName = "Magna",
            GasolineOctaneComposition = 87,
            SatDescription = "Gasolina menor a 92 octanos",
            ProductUm = "LT",
            ProductUmShort = "L",
            ProductColor = "Green"
        };
    }

    private static Product CreatePremiumProduct()
    {
        return new Product
        {
            ProductId = ProductId.Create(1),
            ProductNo = 1,
            ProductName = "Premium",
            GasolineOctaneComposition = 92,
            SatDescription = "Gasolina mayor o igual a 92 octanos",
            ProductUm = "LT",
            ProductUmShort = "L",
            ProductColor = "Red"
        };
    }

    private static Product CreateDieselProduct()
    {
        return new Product
        {
            ProductId = ProductId.Create(3),
            ProductNo = 3,
            ProductName = "Diésel",
            GasolineOctaneComposition = 0,
            SatDescription = "Diésel Automotriz [contenido mayor de azufre a 15 mg/kg y contenido máximo de azufre de 500 mg/kg]",
            ProductUm = "LT",
            ProductUmShort = "L",
            ProductColor = "Black"
        };
    }
}