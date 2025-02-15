namespace Mediator.Pattern;

public class Rcc : IMediator
{
    private Pump _pump;
    private Client _client;
    private List<Task> pendingTasks = [];
    
    public Rcc(Pump pump, Client client)
    {
        _pump = pump;
        _client = client;
        _pump.SetMediator(this);
        _client.SetMediator(this);
    }

    public void Notify<T>(object sender, string @event, T data)
    {
        switch (sender)
        {
            case Client client:
                switch (@event)
                {
                    case "PreSetVolume":
                    {
                        var presetData = data as Client.DispenseFuelData;
                        _pump.PreSet(presetData!.Volume);
                        client.AskForExtraServiceRequest(out var fuelAdditiveData);
                        _pump.Dispense(Product.Premium, fuelAdditiveData);
                        break;
                    }
                    case "CheckTires":
                    {
                        var pressureData = data as Client.CheckTiresData;
                        pendingTasks.Add(CheckTires(pressureData!));
                        break;
                    }
                    case "CleanWindshield":
                    {
                        pendingTasks.Add(CleanWindshield());
                        break;
                    }
                }

                break;
            case Pump pump:
                switch (@event)
                {
                    case "ExecutePendingTasks":
                    {
                        var executePendingTasksData = data as Pump.ExecutePendingTasksData;
                        Task.WaitAll(pendingTasks.ToArray());
                        
                        Console.WriteLine("RCC background tasks executed");

                        do
                            Task.Delay(1000).Wait();
                        while (pump.State == State.Dispensing);

                        pump.OnDispenseComplete();
                        var paymentMethod = _client.GetPaymentMethod();
                        if (paymentMethod == PaymentMethod.Cash)
                            Console.WriteLine($"Payment received, {pump.CalculatePrice()}");
                        else
                        {
                            var terminal = Terminal.GetFirstAvailableTerminal();
                            terminal!.MakeCharge(paymentMethod, pump.CalculatePrice()).Wait();
                        }

                        break;
                    }
                }

                break;
        }
    }

    public void AsignSender<T>(T sender)
    {
        switch (sender)
        {
            case Pump pump:
                _pump = pump;
                break;
            case Client client:
                _client = client;
                break;
        }
    }

    private Task CheckTires(Client.CheckTiresData data)
    {
        return Repeatable.Repeat(() => Console.WriteLine($"Checking tires, pressure: {data.Pressure}"), 5);
    }

    private Task CleanWindshield()
    {
        return Repeatable.Repeat(() => Console.WriteLine("Cleaning windshield"), 5);
    }
}

public class Client : ISender
{
    public PaymentMethod PreferredPaymentMethod { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    IMediator _rcc;

    public Client(string email, string phone)
    {
        Email = email;
        Phone = phone;
    }
    
    public void SetMediator(IMediator mediator)
    {
        _rcc = mediator;
    }

    public void AskForDispenseFuel(decimal volume, Product product)
    {
        Console.WriteLine("Asking for dispense fuel");
        _rcc.Notify(this, "PreSetVolume", new DispenseFuelData(volume, product));
    }

    public void AskForExtraServiceRequest(out FuelAdditiveData? additive)
    {
        _rcc.Notify(this, "CheckTires", new CheckTiresData(32m));
        _rcc.Notify(this, "CleanWindshield", new CleanWindshieldData());
        additive = new FuelAdditiveData("Octane", 0.5m);
    }

    public record DispenseFuelData(decimal Volume, Product Product);

    public record CheckTiresData(decimal Pressure);

    public record CleanWindshieldData;

    public record FuelAdditiveData(string Name, decimal Amount);

    public PaymentMethod GetPaymentMethod() => PreferredPaymentMethod;
}