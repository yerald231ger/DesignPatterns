# Visitor Pattern - Gas Station Operations

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│   GasStation     │
│   (ObjectStructure)│
├──────────────────┤
│ +Accept(visitor) │
│ -elements[]      │
└──────────────────┘
         │ contains
         ↓
┌──────────────────┐
│ IServiceStation  │←─────────────────┐ implements
│   (Element)      │                  │
├──────────────────┤                  │
│ +Accept(visitor) │                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│      Tank        │      │      Pump        │
├──────────────────┤      ├──────────────────┤
│ +Accept(visitor) │      │ +Accept(visitor) │
└──────────────────┘      └──────────────────┘
         │ accepts                    │ accepts
         ↓                            ↓
┌──────────────────┐      ┌──────────────────┐
│    IVisitor      │      │    IVisitor      │
├──────────────────┤      ├──────────────────┤
│ +VisitTank()     │      │ +VisitPump()     │
│ +VisitPump()     │      └──────────────────┘
└──────────────────┘              ↑
         ↑ implements             │ implements
         │            ┌───────────┼──────────┐
         │            │                      │
┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│ AuditorVisitor   │  │ InventoryReporter│  │ MaintenanceVisitor│
├──────────────────┤  ├──────────────────┤  ├──────────────────┤
│ +VisitTank()     │  │ +VisitTank()     │  │ +VisitTank()     │
│ +VisitPump()     │  │ +VisitPump()     │  │ +VisitPump()     │
└──────────────────┘  └──────────────────┘  └──────────────────┘
```

## Explanation:
1. **IServiceStation (Element)**: Interface for accepting visitors
2. **Tank, Pump**: Concrete elements that accept visitors
3. **IVisitor**: Visitor interface with methods for each element type
4. **Concrete Visitors**: AuditorVisitor, InventoryReporter for different operations

## Key Benefit:
Adds new operations to gas station elements without modifying their classes, supporting auditing, reporting, and maintenance.