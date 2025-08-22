# Iterator Pattern - Tank Collection Traversal

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│ ITankCollection  │←─────────────────┐ implements
├──────────────────┤                  │
│ +CreateIterator()│                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ TankCollection   │      │    IIterator     │
├──────────────────┤      ├──────────────────┤
│ +CreateIterator()│      │ +HasNext()       │
│ -tanks[]         │      │ +Next()          │
└──────────────────┘      └──────────────────┘
         │ creates                    ↑ implements
         │                            │
         ↓                            │
┌──────────────────┐      ┌──────────────────┐
│ MaxAlarmIterator │      │ MaxTemperature   │
├──────────────────┤      │    Iterator      │
│ +HasNext()       │      ├──────────────────┤
│ +Next()          │      │ +HasNext()       │
└──────────────────┘      │ +Next()          │
                          └──────────────────┘
                                   │ implements
                          ┌──────────────────┐
                          │ MinVolumeIterator│
                          ├──────────────────┤
                          │ +HasNext()       │
                          │ +Next()          │
                          └──────────────────┘
```

## Explanation:
1. **ITankCollection**: Aggregate interface for creating iterators
2. **TankCollection**: Concrete collection containing tanks
3. **IIterator**: Iterator interface for traversing elements
4. **Concrete Iterators**: MaxAlarmIterator, MaxTemperatureIterator, MinVolumeIterator

## Key Benefit:
Provides different ways to traverse tank collections (by alarm, temperature, volume) without exposing internal collection structure.