# Decorator Pattern - Gas Station Services

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│IGasStationService│←─────────────────┐
├──────────────────┤                  │
│ +PerformService()│                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│   FuelDispense   │      │BaseServiceDecorator│
│   (ConcreteComp) │      │   (Decorator)    │
├──────────────────┤      ├──────────────────┤
│ +PerformService()│      │ +PerformService()│
└──────────────────┘      │ -component       │
                          └──────────────────┘
                                   ↑
                                   │
         ┌─────────────────────────┼─────────────────────────┐
         │                                                 │
┌──────────────────┐                        ┌──────────────────┐
│FilterChangeDecorator│                     │GasolineAdditive  │
├──────────────────┤                        │   Decorator      │
│ +PerformService()│                        ├──────────────────┤
│ +ChangeFilter()  │                        │ +PerformService()│
└──────────────────┘                        │ +AddAdditive()   │
                                           └──────────────────┘
```

## Explanation:
1. **IGasStationService**: Component interface for services
2. **FuelDispense**: Concrete component providing basic fuel dispensing
3. **BaseServiceDecorator**: Base decorator with component reference
4. **Concrete Decorators**: FilterChangeDecorator, GasolineAdditiveDecorator

## Key Benefit:
Dynamically adds new behaviors to fuel dispensing service without modifying existing code, enabling flexible service combinations.