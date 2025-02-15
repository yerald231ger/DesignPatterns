using System.Collections;

namespace Iterator.Pattern.Iterators;

public class MinVolumeIterator : IIterator
{
    private readonly Tank[] _tanks;
    private int _currentPosition;

    public object Current { get; private set; }
    public ITankCollection TankCollection { get; init; }
    public Tank CurrentTank => (Tank)Current;

    public MinVolumeIterator(ITankCollection tankCollection)
    {
        _tanks = tankCollection.GetTanks().OrderBy(t => t.CurrentVolume).ToArray();
        _currentPosition = 0;
        TankCollection = tankCollection;
        Current = _tanks.First();
    }

    public bool MoveNext()
    {
        if (_currentPosition >= _tanks.Length)
        {
            return false;
        }

        Current = _tanks.ElementAt(_currentPosition);
        _currentPosition++;
        return true;
    }

    public void Reset() => _currentPosition = 0;

    IEnumerator<Tank> IEnumerable<Tank>.GetEnumerator() => ((IEnumerable<Tank>)_tanks).GetEnumerator();

    public IEnumerator GetEnumerator() => this;
}