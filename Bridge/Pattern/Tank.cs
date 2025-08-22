using Bridge.Pattern;

namespace Bridge;

public abstract class TankDisplay
{
    protected ITankRenderer _renderer;
    protected string _tankId;
    protected string _product;
    protected decimal _volume;
    protected decimal _capacity;

    protected TankDisplay(ITankRenderer renderer, string tankId, string product, decimal capacity)
    {
        _renderer = renderer;
        _tankId = tankId;
        _product = product;
        _capacity = capacity;
        _volume = 0;
    }

    public virtual void SetVolume(decimal volume)
    {
        _volume = Math.Min(volume, _capacity);
    }

    public abstract void Display();
    
    public virtual void ShowFuelLevel()
    {
        var percentage = (_volume / _capacity) * 100;
        var level = (int)(percentage / 10);
        _renderer.RenderFuelLevel(level, percentage);
    }
}

public class StandardTankDisplay : TankDisplay
{
    public StandardTankDisplay(ITankRenderer renderer, string tankId, string product, decimal capacity)
        : base(renderer, tankId, product, capacity)
    {
    }

    public override void Display()
    {
        _renderer.RenderTank(_tankId, _product, _volume, _capacity);
    }
}

public class DetailedTankDisplay : TankDisplay
{
    private readonly DateTime _lastUpdate;
    private readonly List<string> _alarms;

    public DetailedTankDisplay(ITankRenderer renderer, string tankId, string product, decimal capacity)
        : base(renderer, tankId, product, capacity)
    {
        _lastUpdate = DateTime.Now;
        _alarms = new List<string>();
        
        CheckAlarms();
    }

    public override void SetVolume(decimal volume)
    {
        base.SetVolume(volume);
        CheckAlarms();
    }

    private void CheckAlarms()
    {
        _alarms.Clear();
        var percentage = (_volume / _capacity) * 100;
        
        if (percentage < 10)
            _alarms.Add("LOW FUEL WARNING");
        if (percentage > 95)
            _alarms.Add("HIGH FUEL WARNING");
        if (percentage == 0)
            _alarms.Add("EMPTY TANK");
    }

    public override void Display()
    {
        _renderer.RenderTank(_tankId, _product, _volume, _capacity);
        
        if (_alarms.Any())
        {
            Console.WriteLine("ALARMS:");
            foreach (var alarm in _alarms)
            {
                Console.WriteLine($"⚠️  {alarm}");
            }
        }
        
        Console.WriteLine($"Last Updated: {_lastUpdate:HH:mm:ss}");
    }
}