namespace Memento.Pattern;

public class CalibrationMemento(
    string tankId,
    double currentHeight,
    Dictionary<double, double> calibrationPoints,
    DateTime calibrationDate)
{
    public string TankId { get; } = tankId;
    public double CurrentHeight { get; } = currentHeight;
    public Dictionary<double, double> CalibrationPoints { get; } = calibrationPoints;
    public DateTime CalibrationDate { get; } = calibrationDate;
}