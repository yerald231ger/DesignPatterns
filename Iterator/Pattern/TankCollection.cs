using Iterator.Pattern.Iterators;

namespace Iterator.Pattern;

public class TankList : ITankCollection
{
    private readonly List<Tank> _tanks = [];
    
    public List<Tank> GetTanks() => _tanks;
    public void AddTank(Tank tank) => _tanks.Add(tank);
    public void RemoveTank(Tank tank) => _tanks.Remove(tank);

    public IIterator CreateMaxAlarmIterator() => new MaxAlarmIterator(this);
    public IIterator CreateMinVolumeIterator() => new MinVolumeIterator(this);
    public IIterator CreateMaxTemperatureIterator() => new MaxTemperatureIterator(this);
}