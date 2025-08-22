# Factory Method Pattern - Console Writers

## Structure
```
+------------------+
|     Client       |
+------------------+
         |
         v
+------------------+
|  ConsoleWriter   |<-----------------+
|   (Creator)      |                  |
|------------------|                  |
| +CreateWriter()  |                  |
| +WriteToConsole()|                  |
+------------------+                  |
         ^                            |
         |                            |
+------------------+      +------------------+
| TankConsoleWriter|      | (Other Writers)  |
|   (ConcreteCreator)|     |------------------|
|------------------|      | +CreateWriter()  |
| +CreateWriter()  |      +------------------+
+------------------+
         |
         | creates
         v
+------------------+
|   IWriter        |<-----------------+
|   (Product)      |                  |
|------------------|                  |
| +Write()         |                  |
+------------------+                  |
         ^                            |
         |                            |
+------------------+      +------------------+
|  TankWriter      |      | (Other Products) |
| (ConcreteProduct)|      |------------------|
|------------------|      | +Write()         |
| +Write()         |      +------------------+
+------------------+
```

## Explanation:
1. **ConsoleWriter (Creator)**: Defines factory method for creating writers
2. **TankConsoleWriter**: Concrete creator that creates specific writer types
3. **IWriter (Product)**: Interface for writer objects
4. **TankWriter**: Concrete product with specific writing implementation

## Key Benefit:
Creates writer objects without specifying exact classes, allowing subclasses to choose which writer type to instantiate.