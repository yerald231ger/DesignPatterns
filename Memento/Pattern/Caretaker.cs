namespace Memento.Pattern;

public class Caretaker(string stationName)
{
    private readonly Stack<Station.StationMemento> _mementos = [];

    private Station Station { get; set; } = new(stationName);

    public Station GetStation() => Station;

    private void Save()
    {
        var memento = Station.Save();
        _mementos.Push(memento);
    }

    public void SetTank(Tank tank)
    {
        Save();
        Station.AddTank(tank);
    }

    public void SetTankToHose(Hose hose, Tank tank)
    {
        Save();
        hose.Tank = tank;
    }

    public void SetHoseToPump(Pump pump, Hose hose)
    {
        Save();
        pump.Hoses.Add(hose);
    }

    public void SetPumpToDispenser(Dispenser dispenser, Pump pump)
    {
        Save();
        dispenser.Pumps.Add(pump);
    }

    public void AddDispenser(Dispenser dispenser)
    {
        Save();
        Station.AddDispenser(dispenser);
    }

    public void AddTank(Tank tank)
    {
        Save();
        Station.AddTank(tank);
    }

    public void RemoveDispenser(Dispenser dispenser)
    {
        Save();
        Station.RemoveDispenser(dispenser);
    }

    public void RemoveTank(Tank tank)
    {
        Save();
        Station.RemoveTank(tank);
    }

    public void RemoveTankFromHose(Hose hose)
    {
        Save();
        hose.Tank = null;
    }

    public void RemoveHoseFromPump(Pump pump, Hose hose)
    {
        Save();
        pump.Hoses.Remove(hose);
    }

    public void RemovePumpFromDispenser(Dispenser dispenser, Pump pump)
    {
        Save();
        dispenser.Pumps.Remove(pump);
    }

    public void Undo()
    {
        if (_mementos.Count == 0) return;
        var memento = _mementos.Pop();
        Station.Restore(memento);
    }
}