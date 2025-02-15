using Command.Pattern;

namespace Command;

public record DispenseFuelResult(decimal VolumeDispensed, decimal FinalPrice, Pump.Product Product);

public record DispenseFuelCommand(Pump Pump, Pump.Product Product, decimal Volume) : ICommand<DispenseFuelResult>
{
    public DispenseFuelResult Execute()
    {
        var volumeDispensed = Pump.DispenseFuel(Product, Volume);
        return new DispenseFuelResult(volumeDispensed, Pump.FinalPrice, Product);
    }
}