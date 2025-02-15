using State.Pattern;

namespace State;

public class Mug
{
    private readonly int _capacity;
    private MugState _state;
    private int _currentVolume;

    public Mug(int capacity)
    {
        _state = new EmptyState(this);
        _capacity = capacity;
    }

    private int GetCapacity() => _capacity;
    private void SetState(MugState state) => _state = state;
    public string GetState() => $"{_state.GetType().Name}, current volume: {_currentVolume}";

    public void AddSomeCoffee(int volume)
    {
        _state.AddSomeCoffee(volume);
    }

    public void DrinkSomeCoffee(int volume)
    {
        _state.DrinkSomeCoffee(volume);
    }

    public void EmptyMug() => SetState(new EmptyState(this));


    private void AddCoffee(int volume)
    {
        _currentVolume += volume;
    }

    private void DrinkCoffee(int volume)
    {
        _currentVolume -= volume;
    }

    class EmptyState(Mug mug) : MugState(mug)
    {
        public override void AddSomeCoffee(int volume)
        {
            if (volume > Mug.GetCapacity())
            {
                Mug._currentVolume = Mug.GetCapacity();
                Mug.SetState(new FullState(Mug));
            }

            Mug.AddCoffee(volume);
            Mug.SetState(new PartialFullState(Mug));
        }

        public override void DrinkSomeCoffee(int volume) =>
            Mug.SetState(new EmptyState(Mug));
    }

    class FullState(Mug mug) : MugState(mug)
    {
        public override void AddSomeCoffee(int volume)
            => Mug.SetState(new FullState(Mug));

        public override void DrinkSomeCoffee(int volume)
        {
            if (Mug._currentVolume <= 0)
                Mug.SetState(new EmptyState(Mug));

            Mug.SetState(new PartialFullState(Mug));
            Mug.DrinkCoffee(volume);
        }
    }

    class PartialFullState(Mug mug) : MugState(mug)
    {
        public override void AddSomeCoffee(int volume)
        {
            if (Mug._currentVolume + volume >= Mug.GetCapacity())
            {
                Mug._currentVolume = Mug.GetCapacity();
                Mug.SetState(new FullState(Mug));
            }
            else
                Mug.AddCoffee(volume);
        }

        public override void DrinkSomeCoffee(int volume)
        {
            if (Mug._currentVolume - volume <= 0)
            {
                Mug._currentVolume = 0;
                Mug.SetState(new EmptyState(Mug));
            }
            else
                Mug.DrinkCoffee(volume);
        }
    }
}