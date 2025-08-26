# Design Patterns Implementation - OxxoGas Station Management System

This project demonstrates the implementation of various **Gang of Four (GoF) design patterns** in the context of a gas station management system. Each pattern is implemented as a separate C# console application with real-world examples and use cases.

## Motivation

This project was created to provide practical, real-world examples of design patterns implementation, moving beyond theoretical explanations to show how these patterns solve actual problems in software development. The gas station domain provides a rich context where multiple patterns can be applied and combined effectively.

## Design Patterns Implemented

### Creational Patterns
- **[Abstract Factory](./AbstractFactory/)** - Creates families of related reporter objects
- **[Builder](./Builder/)** - Constructs complex gas station configurations step by step
- **[Factory Method](./FactoryMethod/)** - Creates specific types of console writers
- **[Prototype](./Prototype/)** - Clones existing station configurations
- **[Singleton](./Singleton/)** - Manages global price pool instance

### Structural Patterns
- **[Adapter](./Adapter/)** - Adapts different tank inventory systems
- **[Bridge](./Bridge/)** - Separates tank abstraction from rendering implementation
- **[Composite](./Composite/)** - Handles hierarchical amount calculations
- **[Decorator](./Decorator/)** - Adds additional services to fuel dispensing
- **[Facade](./Facade/)** - Simplifies complex tank inventory operations
- **[Flyweight](./Flywight/)** - Optimizes memory usage for fuel sales data
- **[Proxy](./Proxy/)** - Controls access to service implementations

### Behavioral Patterns
- **[Chain of Responsibility](./ChainOfResponsibility/)** - Handles station alarms in sequence
- **[Command](./Command/)** - Encapsulates pump operations as objects
- **[Iterator](./Iterator/)** - Provides different ways to traverse tank collections
- **[Mediator](./Mediator/)** - Coordinates communication between station components
- **[Memento](./Memento/)** - Saves and restores tank calibration states
- **[Observer](./Observer/)** - Notifies listeners of tank state changes
- **[State](./State/)** - Manages fuel pump state transitions
- **[Strategy](./Strategy/)** - Implements different temperature calculation algorithms
- **[Template Method](./TemplateMethod/)** - Defines skeleton for quote generation processes
- **[Visitor](./Visitor/)** - Performs operations on gas station elements

## Getting Started

### Prerequisites
- .NET 6.0 or later
- Visual Studio 2022 or VS Code (recommended)

### Installation
1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Oxg-designpatterns
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the solution:
   ```bash
   dotnet build
   ```

### Running Examples
Each pattern is implemented as a separate console application. To run a specific pattern:

```bash
cd <PatternName>
dotnet run
```

For example:
```bash
cd Builder
dotnet run
```

## Build and Test

### Building the Solution
```bash
# Build all projects
dotnet build DesignPatternsCs.sln

# Build specific pattern
cd <PatternName>
dotnet build
```

### Running Tests
Currently, this is a demonstration project focused on showcasing pattern implementations. Unit tests can be added for specific patterns as needed.

## Documentation

- **[DESIGN_PATTERNS_GUIDE.md](./DESIGN_PATTERNS_GUIDE.md)** - Detailed documentation with UML diagrams and explanations
- Each pattern folder contains its own README.md with specific implementation details

## Learning Resources

To deepen your understanding of design patterns, we recommend these excellent resources:

### Online Resources
- **[Refactoring.Guru - Design Patterns](https://refactoring.guru/design-patterns)** - Comprehensive guide with examples in multiple languages
- **[Refactoring.Guru - Design Patterns Catalog](https://refactoring.guru/design-patterns/catalog)** - Complete catalog of all GoF patterns
- **[Refactoring.Guru - Pattern Comparison](https://refactoring.guru/design-patterns/relations)** - Understanding relationships between patterns

### Video Learning Series
- **[Design Patterns Video Playlist](https://www.youtube.com/watch?v=mE3qTp1TEbg&list=PLlsmxlJgn1HJpa28yHzkBmUY-Ty71ZUGc)** - Complete video series explaining design patterns with practical examples
- **[Design Patterns in C# Video Series](https://www.youtube.com/watch?v=ybS_d1C_dXE&list=PLZ1XikRjVdB70kLpfpp4h6PsmEVZTt2lZ)** - Design patterns implementation specifically in C# with detailed explanations
- **[Design Patterns Programming Tutorial](https://www.youtube.com/watch?v=v9ejT8FO-7I&list=PLrhzvIcii6GNjpARdnO4ueTUAVR9eMBpc)** - Comprehensive programming tutorial series covering design patterns implementation

### Specific Pattern References
Each implemented pattern can be studied in detail at:
- [Abstract Factory](https://refactoring.guru/design-patterns/abstract-factory) | [Builder](https://refactoring.guru/design-patterns/builder) | [Factory Method](https://refactoring.guru/design-patterns/factory-method) | [Prototype](https://refactoring.guru/design-patterns/prototype) | [Singleton](https://refactoring.guru/design-patterns/singleton)
- [Adapter](https://refactoring.guru/design-patterns/adapter) | [Bridge](https://refactoring.guru/design-patterns/bridge) | [Composite](https://refactoring.guru/design-patterns/composite) | [Decorator](https://refactoring.guru/design-patterns/decorator) | [Facade](https://refactoring.guru/design-patterns/facade) | [Flyweight](https://refactoring.guru/design-patterns/flyweight) | [Proxy](https://refactoring.guru/design-patterns/proxy)
- [Chain of Responsibility](https://refactoring.guru/design-patterns/chain-of-responsibility) | [Command](https://refactoring.guru/design-patterns/command) | [Iterator](https://refactoring.guru/design-patterns/iterator) | [Mediator](https://refactoring.guru/design-patterns/mediator) | [Memento](https://refactoring.guru/design-patterns/memento) | [Observer](https://refactoring.guru/design-patterns/observer) | [State](https://refactoring.guru/design-patterns/state) | [Strategy](https://refactoring.guru/design-patterns/strategy) | [Template Method](https://refactoring.guru/design-patterns/template-method) | [Visitor](https://refactoring.guru/design-patterns/visitor)

## Contributing

Contributions are welcome! If you'd like to:
- Add new pattern implementations
- Improve existing examples  
- Fix bugs or enhance documentation
- Add unit tests

Please feel free to submit a pull request or open an issue.

### Contribution Guidelines
1. Follow the existing code structure and naming conventions
2. Include comprehensive README.md for new patterns
3. Add comments explaining the pattern's intent and key components
4. Ensure examples are realistic and demonstrate practical usage

## License
This project is licensed under the terms specified in the [LICENSE](./LICENSE) file.