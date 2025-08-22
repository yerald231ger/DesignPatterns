# Bridge Pattern - Tank Rendering

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         v
┌──────────────────┐
│      Tank        │
├──────────────────┤
│ +Render()        │
│ +SetRenderer()   │
└──────────────────┘
         │
         │ uses
         v
┌──────────────────┐
│ ITankRenderer    │←─────────────────┐
├──────────────────┤                  │
│ +RenderTank()    │                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│ ConsoleRenderer  │      │ JsonRenderer     │
├──────────────────┤      ├──────────────────┤
│ +RenderTank()    │      │ +RenderTank()    │
└──────────────────┘      └──────────────────┘
                                     │
                          ┌──────────────────┐
                          │ XmlRenderer      │
                          ├──────────────────┤
                          │ +RenderTank()    │
                          └──────────────────┘
```

## Explanation:
1. **Tank**: Abstraction that delegates rendering to ITankRenderer
2. **ITankRenderer**: Bridge interface separating abstraction from implementation
3. **Concrete Renderers**: ConsoleRenderer, JsonRenderer, XmlRenderer
4. **Client**: Can switch rendering implementations at runtime

## Key Benefit:
Separates tank logic from rendering logic, allowing independent variation of both abstractions and implementations.