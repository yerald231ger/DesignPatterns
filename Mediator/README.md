# Mediator Pattern - Station Communication

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│    IMediator     │←─────────────────┐ implements
├──────────────────┤                  │
│ +Notify()        │                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│       Rsc        │      │    Station       │
│(ConcreteMediator)│      │   (Component)    │
├──────────────────┤      ├──────────────────┤
│ +Notify()        │      │ +Send()          │
│ -station         │      │ +Receive()       │
└──────────────────┘      │ -mediator        │
         │                └──────────────────┘
         │ uses                     ↑ uses
         │                         │
         └─────────┬───────────────┘
                   │
         ┌─────────┴─────────┐
         │                   │
┌──────────────────┐  ┌──────────────────┐
│   Component A    │  │   Component B    │
│   (Tank)         │  │   (Pump)         │
├──────────────────┤  ├──────────────────┤
│ +Send()          │  │ +Send()          │
│ +Receive()       │  │ +Receive()       │
└──────────────────┘  └──────────────────┘
```

## Explanation:
1. **IMediator**: Interface for communication between components
2. **Rsc**: Concrete mediator managing station component interactions
3. **Station**: Component that communicates through mediator
4. **Components**: Tanks, Pumps that send/receive messages via mediator

## Key Benefit:
Centralizes complex communications and control logic between station components, reducing dependencies.