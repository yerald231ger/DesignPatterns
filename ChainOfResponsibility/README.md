# Chain of Responsibility Pattern - Station Alarms

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │ uses
         ↓
┌──────────────────┐
│ IHandler         │←─────────────────┐ implements
├──────────────────┤                  │
│ +Handle()        │                  │
│ +SetNext()       │                  │
└──────────────────┘                  │
         ↑ implements                 │
         │                            │
┌─────────────────────────┐      ┌──────────────────┐
│BaseStationAlarmsHandler │      │ ConcreteHandlerB │
├─────────────────────────┤      ├──────────────────┤
│ +Handle()               │      │ +Handle()        │
│ +SetNext()              │      │ +SetNext()       │
└─────────────────────────┘      └──────────────────┘
         │ chains to                  │
         └────────────────────────────┘
                       │ chains to
                       ↓
         ┌──────────────────┐
         │ ConcreteHandler  │
         │       C          │
         ├──────────────────┤
         │ +Handle()        │
         │ +SetNext()       │
         └──────────────────┘
```

## Explanation:
1. **IHandler**: Defines interface for handling requests and chaining
2. **BaseStationAlarmsHandler**: Base handler with chain management logic
3. **Concrete Handlers**: Process specific alarm types or pass to next handler
4. **Client**: Sends alarm requests to first handler in chain

## Key Benefit:
Decouples alarm senders from receivers, allowing multiple handlers to process alarms without tight coupling.