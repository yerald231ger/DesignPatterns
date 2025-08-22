# Flyweight Pattern - Fuel Sales Optimization

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         v
┌──────────────────┐
│ FlyWeightFactory │
├──────────────────┤
│ +GetFlyWeight()  │
│ -flyweights{}    │
└──────────────────┘
         │
         │ creates/manages
         v
┌──────────────────┐
│    FlyWeight     │←─────────────────┐
├──────────────────┤                  │
│ -productName     │                  │
│ -productColor    │                  │ shared
│ -pricePerLiter   │                  │ instances
└──────────────────┘                  │
         ↑                            │
         │                            │
         │ uses                       │
┌──────────────────┐      ┌──────────────────┐
│   FuelSale       │      │   FuelSale       │
├──────────────────┤      ├──────────────────┤
│ +Amount (extrinsic)│     │ +Amount (extrinsic)│
│ +Volume (extrinsic)│     │ +Volume (extrinsic)│
│ +SaleDate (extrinsic)│   │ +SaleDate (extrinsic)│
└──────────────────┘      └──────────────────┘
```

## Intrinsic vs Extrinsic State:
```
Intrinsic (Shared):        Extrinsic (Context):
┌─────────────────┐        ┌──────────────────┐
│ Product Name    │        │ Sale Amount      │
│ Product Color   │        │ Sale Volume      │
│ Price per Liter │        │ Sale Date        │
└─────────────────┘        │ Customer ID      │
                          └──────────────────┘
```

## Explanation:
1. **FlyWeightFactory**: Manages and pools flyweight instances
2. **FlyWeight**: Contains intrinsic state (product info) shared across sales
3. **FuelSale**: Context objects containing extrinsic state (sale-specific data)
4. **Client**: Uses factory to get flyweights and associates them with context

## Key Benefit:
Dramatically reduces memory usage by sharing product information across thousands of fuel sales instead of duplicating it.