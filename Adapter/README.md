# Adapter Pattern - Tank Inventory Systems

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│ITankInventoryAdapter│←─────────────────┐
├──────────────────┤                     │
│ +GetInventory()  │                     │
└──────────────────┘                     │
         ↑                               │
         │                               │
┌──────────────────┐      ┌──────────────────┐
│ ObjectAdapter    │      │ (Other Adapters) │
├──────────────────┤      ├──────────────────┤
│ +GetInventory()  │      │ +GetInventory()  │
│ -adaptee         │      └──────────────────┘
└──────────────────┘
         │
         │ adapts
         ↓
┌──────────────────┐      ┌──────────────────┐
│ FusionAdaptee    │      │ VeederRootAdaptee│
├──────────────────┤      ├──────────────────┤
│ +GetFusionData() │      │ +GetVeederData() │
└──────────────────┘      └──────────────────┘
```

## Explanation:
1. **ITankInventoryAdapter**: Target interface expected by client
2. **ObjectAdapter**: Converts incompatible interfaces to compatible ones
3. **Adaptees**: FusionAdaptee, VeederRootAdaptee with different interfaces
4. **Client**: Works with unified adapter interface

## Key Benefit:
Allows incompatible tank inventory systems to work together through a common interface without modifying existing code.