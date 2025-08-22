# Builder Pattern - Gas Station Management

## Structure
```
┌──────────────────┐
│   Client         │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│ IGasStationDirector │←─────────────────┐ implements
├──────────────────┤                     │
│ +BuildGasStation()│                     │
└──────────────────┘                     │
         ↑ implements                    │
         │                               │
┌──────────────────┐      ┌──────────────────┐
│ StandardDirector │      │ HighVolumeDirector│
├──────────────────┤      ├──────────────────┤
│ +BuildGasStation()│      │ +BuildGasStation()│
└──────────────────┘      └──────────────────┘
         │ uses                          │ uses
         ↓                               ↓
┌──────────────────┐      ┌──────────────────┐
│ StationBuilder   │      │ StationBuilder   │
├──────────────────┤      ├──────────────────┤
│ +CreateDefault() │      │ +CreateDefault() │
│ +Build()         │      │ +Build()         │
└──────────────────┘      └──────────────────┘
         │ creates
         ↓
┌──────────────────┐
│     Station      │
├──────────────────┤
│ +Tanks[]         │
│ +Dispensers[]    │
│ +ErrorMargin     │
└──────────────────┘
```

## Explanation:
1. **IGasStationDirector**: Defines how to build gas stations
2. **Concrete Directors**: StandardDirector, HighVolumeDirector, CompactDirector, TruckStopDirector
3. **StationBuilder**: Constructs Station objects with TankBuilder, DispenserBuilder, etc.
4. **Station**: Complex object with tanks, dispensers, pumps, and hoses

## Key Benefit:
Separates complex object construction from representation, enabling different station configurations using the same building process.