
 1. Design Patterns Used
- Strategy Pattern: Used for different route finding algorithms (shortest path, max stops, exact stops)
- Repository Pattern: Handles data access and graph operations

2. SOLID Principles Followed
- Single Responsibility: Each class handle one part of the functionality
- Open/Closed : New route finding strategies can be added without modifying existing code
- Interface Segregation: Individua/specific interfaces  for different functionalities
- Dependency Inversion: High-level modules depend on abstractions

3. Solution/Project Structure

TrainRouting/
├── Domain/
│   ├── Model/
│   │   ├── Route.cs
│   │   
│   └── Core/
│       ├── IGraph.cs
│       └── IPathFindingStrategy.cs
        └── IFileRepository.cs
├── Data/
│       └── FileRepository.cs
├── Application/
│   ├── Services/
        |__PathfinderStrategy.cs    
│   │   └── RouteService.cs
└── Tests/
    └── RouteFinderTests.cs



 
