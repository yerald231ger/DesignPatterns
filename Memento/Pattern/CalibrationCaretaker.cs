namespace Memento.Pattern;

public class CalibrationCaretaker
{
    private readonly Stack<CalibrationMemento> _mementos = new();
    private readonly TankCalibration _tankCalibration;
    
    public CalibrationCaretaker(TankCalibration tankCalibration)
    {
        _tankCalibration = tankCalibration;
    }
    
    public void SaveState()
    {
        var memento = _tankCalibration.CreateMemento();
        _mementos.Push(memento);
        Console.WriteLine("Calibration state saved");
    }
    
    public void Undo()
    {
        if (_mementos.Count == 0)
        {
            Console.WriteLine("No previous state to restore");
            return;
        }
        
        var memento = _mementos.Pop();
        _tankCalibration.RestoreFromMemento(memento);
        Console.WriteLine("Calibration state restored");
    }
    
    public int SavedStatesCount => _mementos.Count;
}