using Memento.Pattern;

namespace Memento;

public class TankCalibration
{
    public string TankId { get; set; } = string.Empty;
    public double CurrentHeight { get; set; }
    public Dictionary<double, double> CalibrationPoints { get; set; } = new();
    public DateTime CalibrationDate { get; set; } = DateTime.Now;
    
    public void AddCalibrationPoint(double height, double volume)
    {
        CalibrationPoints[height] = volume;
        Console.WriteLine($"Added calibration point: {height}cm -> {volume}L");
    }
    
    public void RemoveCalibrationPoint(double height)
    {
        if (CalibrationPoints.Remove(height))
        {
            Console.WriteLine($"Removed calibration point at height: {height}cm");
        }
    }
    
    public void SetCurrentHeight(double height)
    {
        CurrentHeight = height;
        Console.WriteLine($"Current height set to: {height}cm");
    }
    
    public CalibrationMemento CreateMemento()
    {
        return new CalibrationMemento(TankId, CurrentHeight, new Dictionary<double, double>(CalibrationPoints), CalibrationDate);
    }
    
    public void RestoreFromMemento(CalibrationMemento memento)
    {
        TankId = memento.TankId;
        CurrentHeight = memento.CurrentHeight;
        CalibrationPoints = new Dictionary<double, double>(memento.CalibrationPoints);
        CalibrationDate = memento.CalibrationDate;
        Console.WriteLine($"Restored calibration state for tank {TankId}");
    }
    
    public void DisplayStatus()
    {
        Console.WriteLine($"\nTank: {TankId}");
        Console.WriteLine($"Current Height: {CurrentHeight}cm");
        Console.WriteLine($"Calibration Points: {CalibrationPoints.Count}");
        Console.WriteLine($"Last Updated: {CalibrationDate:HH:mm:ss}");
        
        if (CalibrationPoints.Any())
        {
            Console.WriteLine("Points:");
            foreach (var point in CalibrationPoints.OrderBy(p => p.Key))
            {
                Console.WriteLine($"  {point.Key}cm -> {point.Value}L");
            }
        }
    }
}