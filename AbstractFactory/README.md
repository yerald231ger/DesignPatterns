# Abstract Factory Pattern - Reporter System

## Structure
```
+------------------+
|     Client       |
+------------------+
         |
         v
+------------------+
| IReporterFactory |<-----------------+
|------------------|                  |
| +CreateTankReporter()|              |
+------------------+                  |
         ^                            |
         |                            |
+------------------+      +------------------+
| ReporterFactory  |      | (Future Factories)|
|------------------|      |------------------|
| +CreateTankReporter()|   | +CreateReporters()|
+------------------+      +------------------+
         |
         v
+------------------+
| ITankReporter    |<-----------------+
|------------------|                  |
| +GenerateReport()|                  |
+------------------+                  |
         ^                            |
         |                            |
+------------------+      +------------------+
| TankReporter     |      | (Future Reporters)|
|------------------|      |------------------|
| +GenerateReport()|      | +GenerateReport()|
+------------------+      +------------------+
```

## Explanation:
1. **IReporterFactory**: Abstract factory interface for creating reporter families
2. **ReporterFactory**: Concrete factory that creates tank reporters
3. **ITankReporter**: Abstract product interface for tank reporting
4. **TankReporter**: Concrete reporter implementation for tanks

## Key Benefit:
Creates families of related reporting objects without specifying concrete classes, ensuring compatibility between reporters.