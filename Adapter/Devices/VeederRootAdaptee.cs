namespace Adapter.Devices;

public class VeederRootAdaptee
{
    private readonly string[] _volumes =
    [
        "C01A|01|MAGNA|1000",
        "C01A|02|DIESEL|2000",
        "C01A|03|PREMIUM|3000"
    ];
    
    public string GetTankInventory(string request)
    {
        if (!ValidateRequest(request))
            return string.Empty;
        
        foreach (var volume in _volumes)
        {
            if (volume.Contains(request))
                return volume;
        }
        return string.Empty;
    }
    
    private static bool ValidateRequest(string request)
    {
        return request.Contains("C01A|");
    }
    
}