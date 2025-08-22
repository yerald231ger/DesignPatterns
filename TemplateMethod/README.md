# Template Method Pattern - Quote Generation

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│DefaultTemplateMethod│←─────────────────────┐
│   (AbstractClass)│                     │
├──────────────────┤                     │
│ +GenerateQuote() │                     │
│ +SetupConfig()   │                     │
│ +Calculate()     │                     │
│ +FormatOutput()  │                     │
└──────────────────┘                     │
         ↑                               │
         │                               │
┌──────────────────┐      ┌──────────────────┐
│   BasicQuote     │      │   PineQuote      │
├──────────────────┤      ├──────────────────┤
│ +SetupConfig()   │      │ +SetupConfig()   │
│ +Calculate()     │      │ +Calculate()     │
│ +FormatOutput()  │      │ +FormatOutput()  │
└──────────────────┘      └──────────────────┘
                                    │
                          ┌──────────────────┐
                          │ CherryDynamicQuote│
                          ├──────────────────┤
                          │ +SetupConfig()   │
                          │ +Calculate()     │
                          └──────────────────┘
```

## Template Method Flow:
```
GenerateQuote() {
    1. SetupConfig()    ← Overridden by subclasses
    2. Calculate()      ← Overridden by subclasses  
    3. FormatOutput()   ← Overridden by subclasses
    4. Return result
}
```

## Explanation:
1. **DefaultTemplateMethod**: Defines algorithm skeleton with template method
2. **Abstract Steps**: SetupConfig(), Calculate(), FormatOutput() implemented by subclasses
3. **Concrete Classes**: BasicQuote, PineQuote, CherryDynamicQuote with specific implementations
4. **Client**: Calls template method, which orchestrates the algorithm

## Key Benefit:
Defines algorithm structure while allowing subclasses to customize specific steps without changing overall flow.