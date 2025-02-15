namespace State.Pattern;

public abstract class MugState(Mug mug)
{
    protected readonly Mug Mug = mug;

    public abstract void AddSomeCoffee(int volume);
    public abstract void DrinkSomeCoffee(int volume);
}