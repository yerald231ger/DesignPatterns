# Chain of Responsibility Pattern - Station Alarms

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│ IHandler         │←─────────────────┐
├──────────────────┤                  │
│ +Handle()        │                  │
│ +SetNext()       │                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌─────────────────────────┐      ┌──────────────────┐
│BaseStationAlarmsHandler │      │ ConcreteHandlerB │
├─────────────────────────┤      ├──────────────────┤
│ +Handle()               │      │ +Handle()        │
│ +SetNext()              │      │ +SetNext()       │
└─────────────────────────┘      └──────────────────┘
         │                            │
         └────────────────────────────┘
                       │
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