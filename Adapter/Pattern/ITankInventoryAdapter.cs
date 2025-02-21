namespace Adapter.Pattern;

public interface ITankInventoryAdapter
{
    TankInventory GetTankInventory(int tankId);
}