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
            var data = adaptee.GetTankInventory($"C01A|{tankId.ToString()}");
            var splitData = data.Split('|');
            return new TankInventory(int.Parse(splitData[0]), splitData[1], int.Parse(splitData[2]));
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
            var data = adaptee.GetTankData($"<TankInventory><ID>{tankId.ToString()}</ID></TankInventory>");
        
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

public record TankInventory(int Tank, string Product, decimal Volume);