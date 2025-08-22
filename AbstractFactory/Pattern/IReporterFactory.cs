using System.Reflection;

namespace AbstractFactory.Pattern;

public interface IReporterFactory
{
    public ITankReporter CreateTankReporter();
    public IPumpReporter CreatePumpReporter();
    
    public static List<IReporterFactory> GetFactories()
    {  
        return Assembly.GetCallingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Any(type => typeof(IReporterFactory) == type))
            .Select(t => (IReporterFactory)Activator.CreateInstance(t)!)
            .ToList();
    }
}