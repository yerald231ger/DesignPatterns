# Proxy Pattern - Service Access Control

## Structure
```
┌──────────────────┐
│     Client       │
└──────────────────┘
         │
         ↓
┌──────────────────┐
│    IService      │←─────────────────┐
├──────────────────┤                  │
│ +Operation()     │                  │
└──────────────────┘                  │
         ↑                            │
         │                            │
┌──────────────────┐      ┌──────────────────┐
│   ServiceProxy   │      │  RealService     │
├──────────────────┤      ├──────────────────┤
│ +Operation()     │      │ +Operation()     │
│ +CheckAccess()   │      │ -data            │
│ -realService     │ uses └──────────────────┘
│ -cache           │
└──────────────────┘
```

## Proxy Types Example:
```
Protection Proxy:     Caching Proxy:       Logging Proxy:
┌────────────────┐   ┌────────────────┐   ┌────────────────┐
│ +CheckAuth()   │   │ +CheckCache()  │   │ +LogRequest()  │
│ +Operation()   │   │ +UpdateCache() │   │ +Operation()   │
└────────────────┘   └────────────────┘   └────────────────┘
```

## Explanation:
1. **IService**: Common interface for service and proxy
2. **ServiceProxy**: Controls access to real service with additional functionality
3. **RealService**: Actual service implementation
4. **Client**: Works with proxy transparently

## Key Benefit:
Controls access to service objects, adding security, caching, or logging without changing client code.