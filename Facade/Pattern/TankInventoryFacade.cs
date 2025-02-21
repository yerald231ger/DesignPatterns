using Adapter;
using Adapter.Devices;
using Adapter.Pattern;

namespace Facade.Pattern;

public class TankInventoryFacade
{
    public TankInventory GetTankInventory(string source, int tankId)
    {
        ITankInventoryAdapter? adapter = source switch
        {
            "Fusion" => new TankInventoryFusionAdapter(new FusionAdaptee()),
            "VeederRoot" => new TankInventoryVeederRootAdapter(new VeederRootAdaptee()),
            _ => null
        };
        
        if (adapter == null)
            throw new ArgumentException("Invalid source");

        return adapter.GetTankInventory(tankId);
    }
}