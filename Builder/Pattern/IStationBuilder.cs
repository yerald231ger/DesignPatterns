using System.Reflection;

namespace Builder.Pattern;

public interface IStationBuilder
{
    IInnerBuilder SetInitialData(int stationId, string name, string description);
    void Clear();
    Station Build();
    
    public static List<IStationBuilder> GetBuilders()
    {  
        return Assembly.GetCallingAssembly().GetTypes()
            .Where(t => t.GetInterfaces().Any(type => typeof(IStationBuilder) == type))
            .Select(t => (IStationBuilder)Activator.CreateInstance(t)!)
            .ToList();
    }
}

public interface IInnerBuilder
{
    IInnerBuilder SetLocation(double latitude, double longitude);
    IInnerBuilder SetPl(string pl);
    IInnerBuilder AddTank(int tankId, string product);
    IInnerBuilder AddDispenser(int pumpId, string product);
    void Clear();
    Station Build();
}

public class FreeStationBuilder : IStationBuilder
{
    private Station? _station;
    public void Clear() => _station = null;

    public IInnerBuilder SetInitialData(int stationId, string name, string description)
    {
        _station = new Station(stationId, name, description);
        return new InnerStationBuilder(this);
    }

    class InnerStationBuilder(FreeStationBuilder builder) : IInnerBuilder
    {
        public void Clear() => builder.Clear();
        public Station Build() => builder.Build();

        public IInnerBuilder SetLocation(double latitude, double longitude)
        {
            builder._station!.Latitude = latitude;
            builder._station.Longitude = longitude;
            return this;
        }

        public IInnerBuilder SetPl(string pl)
        {
            builder._station!.Pl = pl;
            return this;
        }

        public IInnerBuilder AddTank(int tankId, string product)
        {
            builder._station!.AddTank(tankId, product);
            return this;
        }

        public IInnerBuilder AddDispenser(int pumpId, string product)
        {
            builder._station!.AddPump(pumpId, product);
            return this;
        }
    }

    public Station Build()
    {
        if (_station == null)
            throw new InvalidOperationException("Station not initialized");

        return _station;
    }
}

public class SmallStationBuilder : IStationBuilder
{
    private Station? _station;
    public void Clear() => _station = null;

    public IInnerBuilder SetInitialData(int stationId, string name, string description)
    {
        _station = new Station(stationId, name, description);
        return new InnerStationBuilder(this);
    }

    class InnerStationBuilder(SmallStationBuilder builder) : IInnerBuilder
    {
        public void Clear() => builder.Clear();
        public Station Build() => builder.Build();

        public IInnerBuilder SetLocation(double latitude, double longitude)
        {
            builder._station!.Latitude = latitude;
            builder._station.Longitude = longitude;
            return this;
        }

        public IInnerBuilder SetPl(string pl)
        {
            builder._station!.Pl = pl;
            return this;
        }

        public IInnerBuilder AddTank(int tankId, string product)
        {
            if (builder._station!.Tanks.Any(t => t.Product == "Diesel"))
                throw new InvalidOperationException("Can't contain any Diesel tank");
            
            builder._station!.AddTank(tankId, product);
            return this;
        }

        public IInnerBuilder AddDispenser(int pumpId, string product)
        {
            if (builder._station!.Pumps.Count >= 2)
                throw new InvalidOperationException("Only two pumps allowed");
            
            if(builder._station!.Pumps.Any(p => p.Product == "Diesel"))
                throw new InvalidOperationException("Can't contain any Diesel pump");
            
            builder._station!.AddPump(pumpId, product);
            return this;
        }
    }

    public Station Build()
    {
        if (_station == null)
            throw new InvalidOperationException("Station not initialized");

        return _station;
    }
}