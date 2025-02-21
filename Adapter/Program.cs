using Adapter;
using Adapter.Devices;
using Adapter.Pattern;

ITankInventoryAdapter fusionAdapter = new TankInventoryFusionAdapter(new FusionAdaptee());
ITankInventoryAdapter fdcAdapter = new TankInventoryVeederRootAdapter(new VeederRootAdaptee());

ValidateTankVolume(fusionAdapter);
ValidateTankVolume(fdcAdapter);

return;

void ValidateTankVolume(ITankInventoryAdapter adapter)
{
    const int tankId = 1;
    var inventory = adapter.GetTankInventory(tankId);

    Console.WriteLine(inventory.Volume < 5
        ? "Low inventory, sent an alert to the manager"
        : "Inventory is good, no alert sent");
}