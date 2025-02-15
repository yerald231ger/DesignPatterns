using System.Collections;

namespace Iterator.Pattern;

public interface IIterator : IEnumerator, IEnumerable<Tank>
{
    public ITankCollection TankCollection { get; init; }
    public Tank CurrentTank { get; }
}