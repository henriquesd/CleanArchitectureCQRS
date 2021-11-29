
# Clean Architecture with CQRS
Web API Project using .NET 6, Clean Architecture (Onion Architecture) with CQRS (Command and Query Responsibility Segregation Pattern) and MediatR

## Technologies
- .NET 6
- MediatR
- Entity Framework 6
- Fluent API
- AutoMapper
- Swagger
- SQL Server

## Command and Query Responsibility Segregation (CQRS)
The Command and Query Responsibility Segregation (CQRS) it’s an architectural pattern where the main focus is to separate the way of reading and writing data. This pattern uses  two separate models:

-   **Commands** — Which are responsible for update data. Commands represent the intention of changing the state of an entity. They execute operations like Insert, Update, Delete. Commands objects alter state and do not return data.
-   **Queries** — Which are responsible for reading data. Commands represent the intention of changing the state of an entity. They execute operations like Insert, Update, Delete. Commands objects alter state and do not return data.

The image below illustrates a basic implementation of the CQRS Pattern:
![cqrs-pattern](https://user-images.githubusercontent.com/11948560/143908776-edbbd0ae-7a15-4fb5-b9c0-11947a456fb3.png)


## Clean Architecture
In Clean architecture, the business logic and application model are in the center of the application (the Core). Instead of having the business logic depending on data access or some other infrastructure concern, the dependency is inverted: the infrastructure and implementation details depend on the Application Core. This can be done by defining abstractions or interfaces in the Application Core, which are then implemented by types defined in the Infrastructure layer.

The image below illustrates this style of architecture:
![onion-architecture](https://user-images.githubusercontent.com/11948560/143908873-37b528a7-b4ef-4143-9bfe-0d91447bc208.png)
*Image source: https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures*

> In this diagram, dependencies flow toward the innermost circle. The Application Core takes its name from its position at the core of this diagram. And you can see on the diagram that the Application Core has no dependencies on other application layers. The application's entities and interfaces are at the very center. Just outside, but still in the Application Core, are domain services, which typically implement interfaces defined in the inner circle. Outside of the Application Core, both the UI and the Infrastructure layers depend on the Application Core, but not on one another (necessarily).


## Project Layers
- **Core Project** - The Core project is the center of the Clean Architecture design, and all other project dependencies should point toward it.
In the Core Project is where all the information related to the Domain will be. The Core project it's free of dependencies, which means that the other layers depends on the Core project, and not the other way around.
- **Infrastructure Project** - In the Infrastructure Project is where all the things that communicate with external stuffs are. This project it's the only place in the system that's know something about data access.
- **Web Project** - The Web Project is the entry point of the application, it is the ASP.NET Core Web project.


## Articles
- [The Command and Query Responsibility Segregation (CQRS) Pattern](https://henriquesd.medium.com/the-command-and-query-responsibility-segregation-cqrs-pattern-16cb7704c809)
