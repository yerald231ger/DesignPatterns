using Adapter;

namespace Proxy.Pattern;

public interface IService
{
    TankInventory GetTankInventory(string source, int tankId);
}