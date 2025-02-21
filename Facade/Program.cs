using Adapter;
using Facade.Pattern;

var facade = new TankInventoryFacade();

var inventory = facade.GetTankInventory("Fusion", 1);

ValidateTankVolume(inventory);

return;

void ValidateTankVolume(TankInventory tankInventory)
{
    Console.WriteLine(tankInventory.Volume < 5
        ? "Low inventory, sent an alert to the manager"
        : "Inventory is good, no alert sent");
}