
# Clean Architecture with CQRS
Web API Project using .NET 6, Clean Architecture (Onion Architecture) with CQRS (Command and Query Responsibility Segregation Pattern) and MediatR.

## Technologies
- [.NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MediatR](https://github.com/jbogard/MediatR)
- [Entity Framework 6](https://docs.microsoft.com/en-us/ef/core/)
- Fluent API
- [AutoMapper](https://automapper.org/)
- [Swagger](https://swagger.io/)
- [SQL Server](https://docs.microsoft.com/en-us/sql/sql-server/?view=sql-server-ver15)

## Clean Architecture
The main idea of the Clean Architecture is the separation of concerns, and this goal can be achieved by dividing the software into layers. In the Clean architecture, the business logic and application model are at the center of the application (the Core). Instead of having the business logic depending on data access or some other infrastructure concern, the dependency is inverted: the infrastructure and implementation details depend on the Application Core. This can be done by defining abstractions or interfaces in the Application Core, which are then implemented by types defined in the Infrastructure layer.

The image below illustrates this style of architecture:
![onion-architecture](https://user-images.githubusercontent.com/11948560/143908873-37b528a7-b4ef-4143-9bfe-0d91447bc208.png)
*Image source: https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures*

> In this diagram, dependencies flow toward the innermost circle. The Application Core takes its name from its position at the core of this diagram. And you can see on the diagram that the Application Core has no dependencies on other application layers. The application's entities and interfaces are at the very center. Just outside, but still in the Application Core, are domain services, which typically implement interfaces defined in the inner circle. Outside of the Application Core, both the UI and the Infrastructure layers depend on the Application Core, but not on one another (necessarily).

As in the image above, the ``Infrastructure`` layer and the ``User Interface`` layer (also known as the ``Presentation`` or ``Web`` layer), conceptually, are on the same level of the hierarchy.

The centre of this architecture is the Core layer. There are different approaches to how the structure of the layers will be inside the solution, but in all of them, the dependencies always flows toward the ``Core``. 
The name of the layers can also be different on each approach, for example, the ``Core`` is also named ``Domain``, etc.
In some approaches, the "Entities" and the "Domain Services" classes are in the same layers (in the ``Core``), and in other approaches, the ``Entities`` are created in the ``Core`` layer, and the ``Domain Services`` are created in another layer.


## Project Layers Overview
In this section there is an explanation about the layers in the solution and in which layer each kind of class should be located.


### Core Project
The Core project is the center of the Clean Architecture design, and all other project dependencies should point toward it.
In the Core Project is where all the information related to the Domain will be. The Core project it's free of dependencies, which means that the other layers depend on the Core project, and not the other way around.

#### What belongs to the Core Project:
- **Interfaces**
- **Entities** - Which is all the things in your system that have an Id. Entities are objects that have their own identity, for example, in a system that has many clients objects, each "Client" object has its own identity, which is not defined by its attributes, but by its Id - it's possible to have two clients with the same name, but their Id will be different.

    The Entity can be an anemic entity or can also contain behaviour. This same Entity can also be used by EF Core to generate the tables in the database, but it's recommended to use the Fluent API in order to configure the EF Core properties for this entity, this way you keep your entity clean, without having configuration details for EF Core (for example, EF Core mapping attributes, etc).

    Another approach is that you can have two different types of Entity: one Entity with behaviour in the Core layer, and another Entity in the Infrastructure layer, with properties only, which will be used by EF Core to generate the tables, then you can configure a mapping between these entities.
- **Aggregates** - It's a Domain-Driven design Pattern for grouping entities together to give you another encapsulation boundary and make it easier to do persistence with groups of related things. For example, for an Order with all the Order Items you can construct this as an aggregate and when you persist it you store the whole order and fetch the whole order.
- **Value Objects** - Are objects that don't need to have their own identification. A Value Object is defined by its properties/attributes values. For a Value Object, if we have two instances with the same values, doesn't matter which one we are going to use, they are interchangeable.
- **Domain Services** - Where the business logic lives, and has to do with multiple entities and value objects and how they work with each other. You can try to put as much behaviour into your entities, aggregates and value objects as you can, but sometimes it doesn't belong in one of those, and so you have domain services for that.
- **Domain Exceptions** - Where custom domain exceptions will be. Instead of relying on a low-level exception like a null reference exception for example (which a developer has to go and debug the system in order to figure out what is the issue), you can throw a domain exception that says something as "Customer not found exception", or "Order doesn't exist exception", or something like this, where it's much more clear what is null, or what is missing, without you having to debug to see what is the problem.
- **Domain Events**
- **Event Handlers**
- **Specifications**
- **Validators** - Any kind of validators that you need for your domain, using something like Fluent Validation, Enums or Smart Enums (that are classes that sort of act like enums).
- **Custom Guards** - Simple validators you do make sure your system is in a consistent state, and you could create your own custom ones that you reuse that apply to your domain model.

### Infrastructure Project
In the Infrastructure Project is where all the things that communicate with external stuffs are. This project it's the only place in the system that's know something about data access.

#### What belongs to the Infrastructure Project:
- **Repositories**
- **DbContext classes**
- **Cached Repositories**
- **API Clients**
- **File System Accessors** (eg. actual file system on your local machine)
- **Azure Storage Accessors** (eg. blob)
- **Emailing / SMS implementations**
- **System Clock**
- **Other Services / Interfaces** - Services/Interfaces that don't use the domain model for their parameters and return types. So for example, if you are using some SDK and you are using some Azure type, you don't want to put this in Core because it would have a dependency on Azure in your Core Project, so you would put this interface or any interface that uses those Azure specific types inside of Infrastructure and then put any of those Services in there as well, but you would typically not call them from outside of Infrastructure.


### Web Project
The Web Project is the entry point of the application, it is the ASP.NET Core Web project.

#### What belongs to the Web Project:
- **API Endpoints**
- **Razor Pages**
- **Controllers**
- **Views**
- **Dtos/ViewModels/API Models**
- **Filters**
- **Model Binders**
- **Tag Helpers**
- **Composition Root**
- **Other Services** - These are going to be services that have parameters or return types that are made up of these types of things that you see inside the Web project (fo example, view models/dtos/etc).
- **Interfaces**

### SharedKernel Project
The SharedKernel is inside of this solution only for demonstration purposes, if you need a SharedKernel, the recommendation is to create it separated from your solution. You can use a SharedKernel Project when you need to share code between multiple bounded contexts. This project can hold common types that you want to share between your different apps. They are typically referenced using your Core Project and ideally distributed using Nuget Packages.

#### What belongs to the SharedKernel Project:
- **Base Entity**
- **Base Value Object**
- **Base Domain Event**
- **Base Specification**
- **Common Interfaces**
- **Common Exceptions**
- **Common Auth** - In case you have the same type of authentication you are using everywhere, you might have certain interfaces that you want to keep here, or user types that you want to keep here.
- **Common Guards**
- **Common Libraries**
- **DI**
- **Logging**
- **Validators**


## Command and Query Responsibility Segregation (CQRS)
The Command and Query Responsibility Segregation (CQRS) it’s an architectural pattern where the main focus is to separate the way of reading and writing data. This pattern uses  two separate models:

-   **Commands** — Which are responsible for update data. Commands represent the intention of changing the state of an entity. They execute operations like Insert, Update, Delete. Commands objects alter state and do not return data.
-   **Queries** — Which are responsible for reading data. Commands represent the intention of changing the state of an entity. They execute operations like Insert, Update, Delete. Commands objects alter state and do not return data.

The image below illustrates a basic implementation of the CQRS Pattern:
![cqrs-pattern](https://user-images.githubusercontent.com/11948560/143908776-edbbd0ae-7a15-4fb5-b9c0-11947a456fb3.png)


## References
-   **Common web application architectures**  
    [https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
-   **The Clean Architecture**  
    [https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html](https://8thlight.com/blog/uncle-bob/2012/08/13/the-clean-architecture.html)
-   **The Onion Architecture**  
    [https://jeffreypalermo.com/blog/the-onion-architecture-part-1/](https://jeffreypalermo.com/blog/the-onion-architecture-part-1/)
-   **Clean Architecture with ASP.NET Core 6 - Steve Smith**  
    [https://youtu.be/lkmvnjypENw](https://youtu.be/lkmvnjypENw)
-   **Clean Architecture Solution Template**  
    [https://github.com/ardalis/cleanarchitecture](https://github.com/ardalis/cleanarchitecture)
-   **CQRS Pattern - Microsoft Documentation**  
    [https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
-   **MediatR**  
    [https://github.com/jbogard/MediatR](https://github.com/jbogard/MediatR)
-   **Onion Architecture in ASP.NET Core**  
    [https://code-maze.com/onion-architecture-in-aspnetcore/](https://code-maze.com/onion-architecture-in-aspnetcore/)

## Articles
- [The Command and Query Responsibility Segregation (CQRS) Pattern](https://henriquesd.medium.com/the-command-and-query-responsibility-segregation-cqrs-pattern-16cb7704c809)
