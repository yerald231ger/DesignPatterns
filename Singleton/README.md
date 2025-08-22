# Singleton Pattern - Price Pool Management

## Structure
```
┌─────────────────┐
│    Client       │
└─────────────────┘
         │
         v
┌──────────────────┐
│    PricePool     │
├──────────────────┤
│ -instance        │
│ -prices[]        │
│ +GetInstance()   │
│ +GetPrice()      │
│ +UpdatePrice()   │
└──────────────────┘
         ^
         │
         │ only one instance
         │
┌──────────────────┐
│ Global Price     │
│ Management       │
└──────────────────┘
```

## Thread-Safe Implementation:
```
Thread A            Thread B
    │                  │
    v                  v
GetInstance()     GetInstance()
    │                  │
    +---> Same Instance <---+
```

## Explanation:
1. **PricePool**: Singleton class ensuring only one instance exists
2. **Private Constructor**: Prevents direct instantiation
3. **GetInstance()**: Static method providing global access point
4. **Client**: Accesses singleton through GetInstance() method

## Key Benefit:
Ensures only one price pool exists globally, providing centralized price management with controlled access.