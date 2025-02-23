using System.Xml.Serialization;
using Adapter.Devices;
using Adapter.Pattern;

namespace Adapter;

public sealed class TankInventoryVeederRootAdapter(VeederRootAdaptee adaptee) : ITankInventoryAdapter
{
    public TankInventory GetTankInventory(int tankId)
    {
        try
        {
            Task.Delay(2120).Wait();
            var data = adaptee.GetTankInventory($"C01A|{tankId:00}");
            var splitData = data.Split('|');
            return new TankInventory(int.Parse(splitData[1]), splitData[2], int.Parse(splitData[3]));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new TankInventory(0, string.Empty, 0);
        }
    }
}

public sealed class TankInventoryFusionAdapter(FusionAdaptee adaptee) : ITankInventoryAdapter
{
    readonly XmlSerializer _serializer = new(typeof(TankInventory));

    public TankInventory GetTankInventory(int tankId)
    {
        try
        {
            Task.Delay(3123).Wait();
            var data = adaptee.GetTankData($"<TankInventory><Id>{tankId:00}</Id></TankInventory>");

            var reader = new StringReader(data);
            var tankInventory = (TankInventory)_serializer.Deserialize(reader)!;

            return tankInventory;
        }
        catch (Exception e)
        {
            return new TankInventory(0, string.Empty, 0);
        }
    }
}

public record TankInventory(int Id, string Product, decimal Volume)
{
    TankInventory() : this(0, string.Empty, 0)
    {
    }
}