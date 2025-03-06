namespace AbstractFactory.Pattern;

public interface IPumpReporter
{
    public void Write(int id, string productOne, string productTwo, decimal productOneVolume, decimal productTwoVolume);
}