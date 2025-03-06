using AbstractFactory.Pattern;
using AbstractFactory.TankWriters;

namespace AbstractFactory.Reporters;

public class ConsoleReporter : IReporterFactory
{
    public ITankReporter CreateTankReporter()
    {
        return new TankReporter(13, 20);
    }

    public IPumpReporter CreatePumpReporter()
    {
        return new PumpReporter();
    }
}

public class FileReporter : IReporterFactory
{
    public ITankReporter CreateTankReporter()
    {
        return new FileTankReporter(26, 20);
    }

    public IPumpReporter CreatePumpReporter()
    {
        return new FilePumpReporter();
    }
}