# Abstract Factory Pattern - Reporter System

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│ IReporterFactory │←─────────────────┐ implements
├──────────────────┤                  │
│ +CreateTankReporter()│               │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ ReporterFactory  │      │ (Future Factories)│
├──────────────────┤      ├──────────────────┤
│ +CreateTankReporter()│   │ +CreateReporters()│
└──────────────────┘      └──────────────────┘
         │ creates
         ↓
┌──────────────────┐
│ ITankReporter    │←─────────────────┐ implements
├──────────────────┤                  │
│ +GenerateReport()│                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ TankReporter     │      │ (Future Reporters)│
├──────────────────┤      ├──────────────────┤
│ +GenerateReport()│      │ +GenerateReport()│
└──────────────────┘      └──────────────────┘
```

## Explanation:
1. **IReporterFactory**: Abstract factory interface for creating reporter families
2. **ReporterFactory**: Concrete factory that creates tank reporters
3. **ITankReporter**: Abstract product interface for tank reporting
4. **TankReporter**: Concrete reporter implementation for tanks

## Key Benefit:
Creates families of related reporting objects without specifying concrete classes, ensuring compatibility between reporters.