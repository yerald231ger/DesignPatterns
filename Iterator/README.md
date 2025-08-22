# Iterator Pattern - Tank Collection Traversal

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│ ITankCollection  │←─────────────────┐
├──────────────────┤                  │
│ +CreateIterator()│                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ TankCollection   │      │    IIterator     │
├──────────────────┤      ├──────────────────┤
│ +CreateIterator()│      │ +HasNext()       │
│ -tanks[]         │      │ +Next()          │
└──────────────────┘      └──────────────────┘
         │                            ↑
         │ creates                    │
         ↓                            │
┌──────────────────┐      ┌──────────────────┐
│ MaxAlarmIterator │      │ MaxTemperature   │
├──────────────────┤      │    Iterator      │
│ +HasNext()       │      ├──────────────────┤
│ +Next()          │      │ +HasNext()       │
└──────────────────┘      │ +Next()          │
                          └──────────────────┘
                                   │
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