using Memento;
using Memento.Pattern;

Console.WriteLine("=== Tank Calibration using Memento Pattern ===\n");

var tankCalibration = new TankCalibration { TankId = "TANK-001" };
var caretaker = new CalibrationCaretaker(tankCalibration);

Console.WriteLine("Starting tank calibration process...");
tankCalibration.DisplayStatus();

Console.WriteLine("\n--- Adding calibration points ---");
caretaker.SaveState();
tankCalibration.AddCalibrationPoint(10, 500);
tankCalibration.AddCalibrationPoint(20, 1200);
tankCalibration.SetCurrentHeight(15);

tankCalibration.DisplayStatus();

Console.WriteLine("\n--- Adding more points ---");
caretaker.SaveState();
tankCalibration.AddCalibrationPoint(30, 2100);
tankCalibration.AddCalibrationPoint(40, 3200);
tankCalibration.SetCurrentHeight(35);

tankCalibration.DisplayStatus();

Console.WriteLine("\n--- Oops! Wrong measurement, let's undo ---");
caretaker.Undo();
tankCalibration.DisplayStatus();

Console.WriteLine("\n--- Let's try again with correct values ---");
caretaker.SaveState();
tankCalibration.AddCalibrationPoint(30, 2000);
tankCalibration.AddCalibrationPoint(40, 3000);
tankCalibration.SetCurrentHeight(32);

tankCalibration.DisplayStatus();

Console.WriteLine("\n--- Another mistake, undo again ---");
caretaker.Undo();
tankCalibration.DisplayStatus();

Console.WriteLine($"\nCalibration complete! We have {caretaker.SavedStatesCount} saved states remaining.");