# Observer Pattern - Tank Notification System

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│   IPublisher     │←─────────────────┐
├──────────────────┤                  │
│ +Subscribe()     │                  │
│ +Unsubscribe()   │                  │
│ +Notify()        │                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│      Tank        │      │   IListener      │
│   (Subject)      │      ├──────────────────┤
├──────────────────┤      │ +Update()        │
│ +Subscribe()     │      └──────────────────┘
│ +Unsubscribe()   │              ↑
│ +Notify()        │              │
│ +ChangeLevel()   │              │
└──────────────────┘    ┌─────────┴──────────┐
         │               │                    │
         │    ┌──────────────────┐  ┌──────────────────┐
         └───→│ EmailListener    │  │ WhatsAppListener │
              ├──────────────────┤  ├──────────────────┤
              │ +Update()        │  │ +Update()        │
              │ +SendEmail()     │  │ +SendWhatsApp()  │
              └──────────────────┘  └──────────────────┘
```

## Explanation:
1. **IPublisher**: Interface for managing subscribers and notifications
2. **Tank (Subject)**: Publishes events when tank level changes
3. **IListener**: Observer interface for receiving notifications
4. **Concrete Listeners**: EmailListener, WhatsAppListener for different notification types

## Key Benefit:
Establishes one-to-many dependency between tank and listeners, enabling automatic notifications without tight coupling.