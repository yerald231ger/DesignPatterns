using System.Collections;

namespace Iterator;

public class Tank
{
    public required string Code { get; set; }
    public required Product Product { get; set; }
    public required TankType TankType { get; set; }
    public required int MinOperativeVolume { get; set; }
    public required int MaxOperativeVolume { get; set; }
    public decimal CurrentVolume { get; set; }
    public decimal Temperature { get; set; }
    private List<string> _alarmCodes = [];
    
    public void AddAlarmCode(string alarmCode)
    {
        _alarmCodes.Add(alarmCode);
    }
    
    public void SetVolume(decimal volume)
    {
        if(volume < MinOperativeVolume || volume > MaxOperativeVolume)
        {
            throw new ArgumentException("Volume out of range");
        }
        
        CurrentVolume = volume;
    }
    
    public void SetTemperature(decimal temperature)
    {
        if(temperature is < -50 or > 50)
        {
            throw new ArgumentException("Temperature out of range");
        }
        
        Temperature = temperature;
    }
    
    public ICollection<string> GetAlarmCodes() => _alarmCodes;

    public static Tank CreateDefaultTank(string code, Product product)
    {
        return new Tank
        {
            Code = code,
            Product = product,
            TankType = TankType.HorizontalCylinder,
            MinOperativeVolume = 100,
            MaxOperativeVolume = 1000,
            CurrentVolume = 0,
            Temperature = 25,
            _alarmCodes = []
        };
    }

    public override string ToString() => $"Tank: {Code}, Product: {Product}, Volume: {CurrentVolume}, Temperature: {Temperature}, Alarms count: {_alarmCodes.Count}";
}

public enum Product
{
    Premium,
    Magna,
    Diesel
}

public enum TankType
{
    HorizontalCylinder,
    VerticalCylinder,
    Rectangular
}