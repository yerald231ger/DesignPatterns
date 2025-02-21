namespace Adapter.Devices;

public class FusionAdaptee 
{
    private readonly string[] _volumes =
    [
        "<TankInventory><ID>01</ID><Product>MAGNA</Product><Volume>1000</Volume></TankInventory>",
        "<TankInventory><ID>02</ID><Product>DIESEL</Product><Volume>2000</Volume></TankInventory>",
        "<TankInventory><ID>03</ID><Product>PREMIUM</Product><Volume>3000</Volume></TankInventory>"
    ]; 
    
    public string GetTankData(string request)
    {
        if (!ValidateRequest(request))
            return string.Empty;
        
        var tankId = int.TryParse(request.AsSpan(request.IndexOf("<ID>", StringComparison.Ordinal) + 4, 2), out var id) ? id : 0;
        
        if(tankId == 0)
            return string.Empty;
        
        foreach (var volume in _volumes)
        {
            if (volume.Contains($"<ID>{tankId}</ID>"))
                return volume;
        }
        
        return string.Empty;
    }
    
    private static bool ValidateRequest(string request)
    {
        return request.Contains("<TankInventory><ID>") && request.Contains("</ID></TankInventory>");
    }
}