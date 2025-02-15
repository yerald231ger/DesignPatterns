using Command.Commands;
using Command.Pattern;

namespace Command;

public class Client
{
    private ICommand<DispenseFuelResult> _dispenseFuelCommand;
    private ICommand<CleanWindshieldResult> _cleanWindshieldCommand;
    public decimal CurrentTankVolume { get; set; } = 2;

    public void SelfService(Pump pump, Pump.Product product, decimal volumeToDispense)
    {
        var volumeDispensed = pump.DispenseFuel(product, volumeToDispense);
        CurrentTankVolume += volumeDispensed;
        Console.WriteLine($"Dispensed {volumeDispensed} liters of {product}.");
        Console.WriteLine($"Current tank volume: {CurrentTankVolume} liters.");
        Console.WriteLine($"Final price: {pump.FinalPrice}.");
    }

    public void SetCommand(ICommand<DispenseFuelResult> command)
    {
        _dispenseFuelCommand = command;
    }
    
    public void SetCommand(ICommand<CleanWindshieldResult> command)
    {
        _cleanWindshieldCommand = command;
    }

    public void RequestFuelDispense()
    {
        var result = _dispenseFuelCommand.Execute();
        CurrentTankVolume += result.VolumeDispensed;
        Console.WriteLine($"Dispensed {result.VolumeDispensed} liters of {result.Product}.");
        Console.WriteLine($"Current tank volume: {CurrentTankVolume} liters.");
        Console.WriteLine($"Final price: {result.FinalPrice}.");
    }
    
    public void RequestCleanWindshield()
    {
        var result = _cleanWindshieldCommand.Execute();
        Console.WriteLine($"Windshield clean? {result.IsCleaned}.");
    }
    
    public void CleanWindshield()
    {
        Console.WriteLine("Cleaning windshield...");
    }
}