# Strategy Pattern - Temperature Compensation Calculation

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│      Tank        │
│   (Context)      │
├──────────────────┤
│ +Calculate()     │
│ +SetStrategy()   │
│ -strategy        │
└──────────────────┘
         │
         │ uses
         ↓
┌──────────────────┐
│ICalculateTcStrategy│←─────────────────┐
│   (Strategy)     │                   │
├──────────────────┤                   │
│ +Calculate()     │                   │
└──────────────────┘                   │
         ↑                             │
         │                             │
┌──────────────────┐      ┌──────────────────┐
│ GasolineStrategy │      │ DieselStrategy   │
├──────────────────┤      ├──────────────────┤
│ +Calculate()     │      │ +Calculate()     │
└──────────────────┘      └──────────────────┘
                                    │
                          ┌──────────────────┐
                          │ EthanolStrategy  │
                          ├──────────────────┤
                          │ +Calculate()     │
                          └──────────────────┘
```

## Strategy Selection:
```
ProductType.Gasoline  ───> GasolineStrategy
ProductType.Diesel    ───> DieselStrategy
ProductType.Ethanol   ───> EthanolStrategy
```

## Explanation:
1. **Tank (Context)**: Uses strategy for temperature compensation calculations
2. **ICalculateTcStrategy**: Strategy interface defining calculation method
3. **Concrete Strategies**: GasolineStrategy, DieselStrategy, EthanolStrategy
4. **Client**: Sets appropriate strategy based on product type

## Key Benefit:
Enables different temperature compensation algorithms for different fuel types without modifying tank logic.