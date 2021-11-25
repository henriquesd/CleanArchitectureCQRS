
# Clean Architecture

## Core Project
The Core project is the center of the Clean Architecture design, and all other project dependencies should point toward it.
In the Core Project is where all the information related to the Domain will be. The Core project it's free of dependencies, which means that the other layers depends on the Core project, and not the other way around.

**What belongs to the Core Project:**
- **Interfaces**
- **Entities** - Which is all the things in your system that have an id.
- **Aggregates** - It's a Domain-Driven design Pattern for grouping entities together to give you another encapsulation boundary and make it easier to do persistence with groups of related things. For example, for an Order with all the Order Items you can construct this as an aggregate and when you persist it you store the whole order and fetch the whole order.
- **Value Objects** - Things that don't have an identity and that you can compare just by looking at their properties. For example, the DateTime in .NET.
- **Domain Services** - Where the business logic lives, and has to do with multiple entities and value objects and how they work with each other. You can try to put as much behaviour into your entities, aggregates and value objects as you can, but sometimes it doesn't belong in one of those, and so you have domain services for that.
- **Domain Exceptions** - Where custom domain exceptions will be. Instead of relying on a low-level exception like a null reference exception for example (which a developer has to go and debug the system in order to figure out what is the issue), you can throw a domain exception that says something as "Customer not found exception", or "Order doesn't exist exception", or something like this, where it's much more clear what is null, or what is missing, without you having to debug to see what is the problem.
- **Domain Events**
- **Event Handlers**
- **Specifications**
- **Validators** - Any kind of validators that you need for your domain, using something like Fluent Validation, Enums or Smart Enums (that are classes that sort of act like enums).
- Custom Guards- Simple validators you do make sure your system is in a consistent state, and you could create your own custom ones that you reuse that apply to your domain model.

## **Infrastructure Project**
In the Infrastructure Project is where all the things that communicate with external stuffs are. This project it's the only place in the system that's know something about data access.
	
**What belongs to the Infrastructure Project:**
- **Repositories**
- **DbContext classes**
- **Cached Repositories**
- **API Clients**
- **File System Accessors** (eg. actual file system on your local machine)
- **Azure Storage Accessors** (eg. blob)
- **Emailing / SMS implementations**
- **System Clock**
- **Other Services / Interfaces** - Services/Interfaces that don't use the domain model for their parameters and return types. So for example, if you are using some SDK and you are using some Azure type, you don't want to put this in Core because it would have a dependency on Azure in your Core Project, so you would put this interface or any interface that uses those Azure specific types inside of Infrastructure and then put any of those Services in there as well, but you would typically not call them from outside of Infrastructure.

## **Web Project**
The Web Project is the entry point of the application, it is the ASP.NET Core Web project.

**What belongs to the Web Project:**
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
  
  
## **SharedKernel Project**
The SharedKernel is inside of this solution only for demonstration purpose, but it's recommended to have it separated. You can use a SharedKernel Project when you need to share code between multiple bounded contexts.
When you need to share code between applications, DDD uses the term "Shared Kernel". This project holds common types that you want to share between your different apps. They are typically referenced using your Core Project and ideally distributed using Nuget Packages.

Infrastructure Dependencies must not be in the Shared Kernel project, because all your different Core projects in all your different solutions are going to depend on this, and  we must keep that the Core project (your business logic) from having any dependencies on SharedKernel.

**What belongs to the SharedKernel Project:**
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

