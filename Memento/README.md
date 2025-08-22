# Memento Pattern - Station State Management

## Structure
```
+------------------+
|     Client       |
+------------------+
         |
         v
+------------------+
|    Caretaker     |
|------------------|
| +Backup()        |
| +Restore()       |
| -mementos[]      |
+------------------+
         |
         v
+------------------+
|    Station       |<-----------------+
|   (Originator)   |                  |
|------------------|                  |
| +CreateMemento() |                  |
| +RestoreMemento()|                  |
| -state           |                  |
+------------------+                  |
         |                            |
         | creates                    |
         v                            |
+------------------+      +------------------+
|    IMemento      |      | ConcreteMemento  |
|   (Interface)    |      |------------------|
|------------------|      | +GetState()      |
| +GetState()      |      | -state           |
+------------------+      +------------------+
```

## State Snapshot Example:
```
Station State:
+------------------+
| TankLevels[]     |
| PumpStatus[]     |
| AlarmStates[]    |
| Timestamp        |
+------------------+
```

## Explanation:
1. **Station (Originator)**: Creates and restores from mementos
2. **IMemento**: Interface for memento objects storing state
3. **ConcreteMemento**: Stores station state snapshot
4. **Caretaker**: Manages memento storage and retrieval

## Key Benefit:
Captures and restores station state without violating encapsulation, enabling undo/redo functionality.