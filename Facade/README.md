# Facade Pattern - Tank Inventory Management

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│TankInventoryFacade│
├──────────────────┤
│ +GetInventory()  │
│ +UpdateTank()    │
│ +GenerateReport()│
└──────────────────┘
         │
         │ uses
         ↓
┌──────────────────┐    ┌──────────────────┐    ┌──────────────────┐
│   TankReader     │    │   TankWriter     │    │  ReportGenerator │
├──────────────────┤    ├──────────────────┤    ├──────────────────┤
│ +ReadLevel()     │    │ +WriteData()     │    │ +CreateReport()  │
│ +ReadTemperature()│    │ +UpdateStatus()  │    │ +FormatData()    │
└──────────────────┘    └──────────────────┘    └──────────────────┘
         │ uses                  │ uses                  │ uses
         ↓                       ↓                       ↓
┌──────────────────┐    ┌──────────────────┐    ┌──────────────────┐
│   Database       │    │   FileSystem     │    │   EmailService   │
├──────────────────┤    ├──────────────────┤    ├──────────────────┤
│ +Query()         │    │ +WriteFile()     │    │ +SendEmail()     │
└──────────────────┘    └──────────────────┘    └──────────────────┘
```

## Explanation:
1. **TankInventoryFacade**: Simplified interface hiding complex subsystem
2. **Subsystems**: TankReader, TankWriter, ReportGenerator with complex operations
3. **External Systems**: Database, FileSystem, EmailService
4. **Client**: Uses simple facade methods instead of complex subsystem calls

## Key Benefit:
Provides a simple interface to complex tank inventory operations, hiding implementation details from clients.