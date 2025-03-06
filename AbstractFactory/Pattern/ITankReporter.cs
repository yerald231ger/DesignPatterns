namespace AbstractFactory.TankWriters;

public interface ITankReporter
{
    public void Write(int id, string product, decimal volume, decimal capacity);
}