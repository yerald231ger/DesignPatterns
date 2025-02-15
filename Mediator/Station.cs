using Mediator.Pattern;

namespace Mediator;

public class Pump : ISender
{
    private IMediator _rcc;

    public void SetMediator(IMediator rcc)
    {
        _rcc = rcc;
    }

    const decimal _pricePerLiter = 20.4m;
    public State State { get; private set; }
    public decimal Odometer { get; set; }
    public decimal VolumeToDispense { get; set; }

    public void PreSet(decimal volume)
    {
        State = State.Idle;
        VolumeToDispense = volume;
        Console.WriteLine($"Preset volume: {volume}");
    }

    public void Dispense(Product product, Client.FuelAdditiveData? additive)
    {
        if (additive != null)
        {
            Console.WriteLine($"Adding {additive.Amount} of {additive.Name}");
        }

        State = State.Dispensing;
        Task.Run(async () =>
        {
            await Repeatable.Repeat(() => Console.WriteLine("Dispensing..."), 60);
            State = State.Stopped;
        });
        _rcc.Notify(this, "ExecutePendingTasks", new ExecutePendingTasksData());
    }

    public void OnDispenseComplete()
    {
        State = State.Idle;
        Console.WriteLine("Dispensing complete, Take the hose back");
    }

    public decimal CalculatePrice() => VolumeToDispense * _pricePerLiter;

    public record ExecutePendingTasksData;
}

public class Terminal
{
    private static readonly List<Terminal> _terminals = [];

    public required int Id { get; init; }
    private bool IsInService { get; set; }

    public async Task MakeCharge(PaymentMethod paymentMethod, decimal price)
    {
        IsInService = true;
        Console.WriteLine($"Making charge of {price} with {paymentMethod}");
        await Task.Delay(1500);
    }

    public static void CreateTerminalDefaultTerminals()
    {
        _terminals.Add(new Terminal { Id = 1 });
        _terminals.Add(new Terminal { Id = 2 });
    }

    public static Terminal? GetFirstAvailableTerminal() =>
        _terminals.FirstOrDefault(t => t.IsInService);
}

public enum PaymentMethod
{
    Cash,
    CreditCard,
    DebitCard
}

public enum State
{
    Idle,
    Dispensing,
    Stopped
}

public enum Product
{
    Magna,
    Premium,
    Diesel
}

public abstract class Repeatable
{
    public static Task Repeat(Action action, int times)
    {
        return Task.Run(() =>
        {
            for (var i = 0; i < times; i++)
            {
                action();
                Task.Delay(1000).Wait();
            }
        });
    }
}