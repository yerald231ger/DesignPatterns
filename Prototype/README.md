# Prototype Pattern - Station Cloning

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         v
┌──────────────────┐
│ ICloneable<T>    │←─────────────────┐ implements
├──────────────────┤                  │
│ +Clone()         │                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ Station          │      │ Tank             │
├──────────────────┤      ├──────────────────┤
│ +Clone()         │      │ +Clone()         │
│ +Tanks[]         │      │ +ProductId       │
│ +Dispensers[]    │      │ +UtilCapacity    │
└──────────────────┘      └──────────────────┘
         │ implements                 │ implements
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ Dispenser        │      │ Product          │
├──────────────────┤      ├──────────────────┤
│ +Clone()         │      │ +Clone()         │
│ +Pumps[]         │      │ +ProductName     │
└──────────────────┘      └──────────────────┘
```

## Explanation:
1. **ICloneable<T>**: Generic prototype interface for cloning objects
2. **Station**: Prototype that can clone itself and nested objects
3. **Tank, Dispenser, Product**: Cloneable components of a station
4. **Client**: Creates new objects by cloning existing prototypes

## Key Benefit:
Creates new objects by copying existing instances, avoiding expensive initialization and enabling template-based object creation.