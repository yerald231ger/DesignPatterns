namespace Builder.Models;

public class Station
{
    public List<Tank> Tanks { get; set; } = [];
    public List<Dispenser> Dispensers { get; set; } = [];
    public decimal ErrorMargin { get; set; } = 0.05m;
}

public class Tank
{
    public short TankId { get; set; }
    public ProductId ProductId { get; set; }
    public Product? Product { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal UtilCapacity { get; set; }
    public decimal OperatingCapacity { get; set; }
    public decimal FondageCapacity { get; set; }
    public decimal TotalStorageCapacity { get; set; }
    public decimal MinimumOperatingVolume { get; set; }
    public DateTime CalibrationDate { get; set; }
    public bool IsActive { get; set; }
    public TankReading? FirstTankReading { get; set; }
    public TankReading? LastTankReading { get; set; }
    public List<ReceptionInventory> ReceptionInventories { get; set; } = [];
}

public class TankReading
{
    public short TankId { get; set; }
    public int ReadingId { get; set; }
    public decimal CurrentVolume { get; set; }
    public decimal CurrentTcVolume { get; set; }
    public decimal Temperature { get; set; }
    public DateTime ReadingDate { get; set; }
    public string? IntegrityHash { get; set; }
    public ProductId ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
}

public class Dispenser
{
    public Guid Id { get; set; }
    public short DispenserId { get; set; }
    public string MeasurementSystem { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MeasurementUncertainty { get; set; }
    public DateTime CalibrationValidity { get; set; }
    public bool IsActive { get; set; }
    public DateTime LastCalibrationDate { get; set; }
    public short ValidityTime { get; set; }
    public List<Pump> Pumps { get; set; } = [];
}

public class Pump
{
    public Guid Id { get; set; }
    public short PumpId { get; set; }
    public short PumpNumber { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<Hose> Hoses { get; set; } = [];
}

public class Hose
{
    public Guid Id { get; set; }
    public short HoseNumber { get; set; }
    public short TankId { get; set; }
    public Tank? Tank { get; set; }
    public ProductId ProductId { get; set; }
    public Product? Product { get; set; }
    public List<DeliveryInventory> DeliveryInventories { get; set; } = [];
}

public class Product
{
    public ProductId ProductId { get; set; }
    public short ProductNo { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int GasolineOctaneComposition { get; set; }
    public string SatDescription { get; set; } = string.Empty;
    public string ProductUm { get; set; } = string.Empty;
    public string ProductUmShort { get; set; } = string.Empty;
    public string ProductColor { get; set; } = string.Empty;
}

public readonly struct ProductId
{
    public short Value { get; }
    
    private ProductId(short value)
    {
        Value = value;
    }
    
    public static ProductId Create(short value) => new(value);
    
    public static implicit operator short(ProductId productId) => productId.Value;
    public static implicit operator ProductId(short value) => new(value);
    
    public override string ToString() => Value.ToString();
}

public class ReceptionInventory
{
    public int ReceptionInventoryId { get; set; }
    public short TankId { get; set; }
    public decimal ReceivedVolume { get; set; }
    public DateTime ReceptionDateTime { get; set; }
    public string DocumentNumber { get; set; } = string.Empty;
}

public class DeliveryInventory
{
    public int DeliveryInventoryId { get; set; }
    public decimal DeliveredAmount { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
    public short SaleTypeId { get; set; }
    public decimal DeliveredVolume { get; set; }
    public DateTime InterfaceTransactionDateTime { get; set; }
}

public class DetailedDeliveryInventory
{
    public int DeliveryInventoryId { get; set; }
    public decimal DeliveredAmount { get; set; }
    public short PumpNumber { get; set; }
    public short HoseNumber { get; set; }
    public short SaleTypeId { get; set; }
    public decimal DeliveredVolume { get; set; }
    public DateTime InterfaceTransactionDateTime { get; set; }
}