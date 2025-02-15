using System.Collections.ObjectModel;
using Microsoft.VisualBasic;

namespace Iterator.Pattern;

public interface ITankCollection
{
    public List<Tank> GetTanks();
    public abstract IIterator CreateMaxAlarmIterator();
    // public ITankIterator CreateMinVolumeIterator();
    // public ITankIterator CreateMaxVolumeIterator();
}