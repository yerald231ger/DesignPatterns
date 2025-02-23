using Adapter;
using Facade.Pattern;
using Microsoft.Extensions.Caching.Memory;
using Proxy.Pattern;

namespace Proxy;

public class Proxy : IService
{
    readonly TankInventoryFacade _facade = new();
    readonly MemoryCache _cache = new(new MemoryCacheOptions());

    public TankInventory GetTankInventory(string source, int tankId)
    {
        var tankInventory = _cache.GetOrCreate($"{source}-{tankId}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
            return _facade.GetTankInventory(source, tankId);
        });
        
        return tankInventory ?? new TankInventory(0,string.Empty, 0);
    }
}

public class Facade : TankInventoryFacade, IService
{
    readonly TankInventoryFacade _facade = new();

    public new TankInventory GetTankInventory(string source, int tankId)
    {
        return _facade.GetTankInventory(source, tankId);
    }
}