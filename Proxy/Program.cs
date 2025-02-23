using Adapter;
using Proxy.Pattern;

IService facade = new Proxy.Proxy();

var inventory = facade.GetTankInventory("Fusion", 1);
inventory = facade.GetTankInventory("Fusion", 2);
inventory = facade.GetTankInventory("Fusion", 1);

ValidateTankVolume(inventory);

return;

void ValidateTankVolume(TankInventory tankInventory)
{
    Console.WriteLine(tankInventory.Volume < 5
        ? "Low inventory, sent an alert to the manager"
        : "Inventory is good, no alert sent");
}