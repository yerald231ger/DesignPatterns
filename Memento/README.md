# Memento Pattern - Tank Calibration State Management

## Structure
```
┌──────────────────────┐
│       Client         │
└──────────────────────┘
           │ uses
           ↓
┌──────────────────────┐
│ CalibrationCaretaker │
├──────────────────────┤
│ +SaveState()         │
│ +Undo()              │
│ -mementos[]          │
└──────────────────────┘
           │ manages
           ↓
┌──────────────────────┐←─────────────────────┐ 
│   TankCalibration    │                      │
│    (Originator)      │                      │
├──────────────────────┤                      │
│ +CreateMemento()     │                      │
│ +RestoreFromMemento()│                      │
│ +AddCalibrationPoint()│                     │
│ +SetCurrentHeight()  │                      │
│ -calibrationPoints[] │                      │
│ -currentHeight       │                      │
└──────────────────────┘                      │
           │                                  │
           │ creates                          │
           ↓                                  │
┌──────────────────────┐          ┌──────────────────────┐
│ CalibrationMemento   │          │   Calibration Data   │
├──────────────────────┤          ├──────────────────────┤
│ +TankId              │          │ Height → Volume      │
│ +CurrentHeight       │          │ 10cm → 500L          │
│ +CalibrationPoints   │          │ 20cm → 1200L         │
│ +CalibrationDate     │          │ 30cm → 2000L         │
└──────────────────────┘          └──────────────────────┘
```

## Calibration State Example:
```
Tank Calibration State:
┌──────────────────────┐
│ TankId: "TANK-001"   │
│ CurrentHeight: 25cm  │
│ CalibrationPoints:   │
│   10cm → 500L        │
│   20cm → 1200L       │
│   30cm → 2000L       │
│ Date: 14:35:22       │
└──────────────────────┘
```

## Explanation:
1. **TankCalibration (Originator)**: Creates and restores calibration states
2. **CalibrationMemento**: Stores complete calibration snapshot
3. **CalibrationCaretaker**: Manages memento storage for undo operations
4. **Client**: Calibration technician performing tank measurements

## Key Benefit:
Allows calibration technicians to safely experiment with measurements and undo mistakes during the precision-critical calibration process.