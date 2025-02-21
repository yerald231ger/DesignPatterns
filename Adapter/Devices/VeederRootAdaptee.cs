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
        
        var tankId = int.TryParse(request.AsSpan(request.IndexOf("C01A|", StringComparison.Ordinal) + 5, 2), out var id) ? id : 0;
        
        if(tankId == 0)
            return string.Empty;
        
        foreach (var volume in _volumes)
        {
            var parts = volume.Split('|');
            if (int.Parse(parts[0]) == tankId)
                return volume;
        }
        return string.Empty;
    }
    
    private static bool ValidateRequest(string request)
    {
        return request.Contains("C01A|");
    }
    
}