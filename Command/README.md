# Command Pattern - Pump Operations

## Structure
```
+------------------+
|     Client       |
+------------------+
         |
         v
+------------------+
|      Rcc         |
|   (Remote)       |
|------------------|
| +ExecuteCommand()|
| +UndoCommand()   |
+------------------+
         |
         v
+------------------+
|    ICommand      |<-----------------+
|------------------|                  |
| +Execute()       |                  |
| +Undo()          |                  |
+------------------+                  |
         ^                            |
         |                            |
+------------------+      +------------------+
|DispenseFuelCommand|     |CleanWindshield   |
|------------------|      |    Command       |
| +Execute()       |      |------------------|
| +Undo()          |      | +Execute()       |
| -pump            |      | +Undo()          |
+------------------+      +------------------+
         |
         v
+------------------+
|      Pump        |
|   (Receiver)     |
|------------------|
| +DispenseFuel()  |
| +CleanWindshield()|
+------------------+
```

## Explanation:
1. **ICommand**: Interface for executing operations and undo functionality
2. **Concrete Commands**: DispenseFuelCommand, CleanWindshieldCommand
3. **Rcc (Remote)**: Invoker that triggers commands
4. **Pump (Receiver)**: Object that performs the actual work

## Key Benefit:
Encapsulates pump operations as objects, enabling undo functionality, queuing, and logging of operations.