namespace Adapter.Devices;

public class FusionAdaptee 
{
    private readonly string[] _volumes =
    [
        "<TankInventory><Id>01</Id><Product>MAGNA</Product><Volume>1000</Volume></TankInventory>",
        "<TankInventory><Id>02</Id><Product>DIESEL</Product><Volume>2000</Volume></TankInventory>",
        "<TankInventory><Id>03</Id><Product>PREMIUM</Product><Volume>3000</Volume></TankInventory>"
    ]; 
    
    public string GetTankData(string request)
    {
        if (!ValidateRequest(request))
            return string.Empty;
        
        foreach (var volume in _volumes)
        {
            if (volume.Contains($"<Id>{request.Substring(19, 2)}</Id>"))
                return volume;
        }
        
        return string.Empty;
    }
    
    private static bool ValidateRequest(string request)
    {
        return request.Contains("<TankInventory><Id>") && request.Contains("</Id></TankInventory>");
    }
}