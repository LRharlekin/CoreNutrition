Live project can be viewed at:
https://core-nutrition.azurewebsites.net

Swagger UI:
https://core-nutrition.azurewebsites.net/swagger/index.html

**Table of Contents**

- [DB Schema / ERD](#db-schema--erd)
- [Clean Architecture](#clean-architecture)
- [Domain-Driven Design](#domain-driven-design)
  - [Eventual Consistency; Aggregates as Transactional Boundaries](#eventual-consistency-aggregates-as-transactional-boundaries)
- [E-Commerce Project Architecture](#e-commerce-project-architecture)
  - [Domain Layer](#domain-layer)
    - [Rationale for strongly typed IDs as value objects](#rationale-for-strongly-typed-ids-as-value-objects)
      - [Benefits of strongly typed IDs](#benefits-of-strongly-typed-ids)
      - [Benefits of defining strongly typed IDs as value objects](#benefits-of-defining-strongly-typed-ids-as-value-objects)
    - [DDD base classes and interfaces](#ddd-base-classes-and-interfaces)
      - [The `ValueObject` base class](#the-valueobject-base-class)
      - [The `Entity` base class](#the-entity-base-class)
      - [The `AggregateRoot` base class](#the-aggregateroot-base-class)
      - [The `AggregateRootId` and `EntityId` base classes](#the-aggregaterootid-and-entityid-base-classes)
      - [The `IDomainEvent` and `IHasDomainEvent` interfaces](#the-idomainevent-and-ihasdomainevent-interfaces)
  - [Application Layer](#application-layer)
  - [Presentation Layer](#presentation-layer)
    - [Contracts Project (classlib)](#contracts-project-classlib)
    - [Api Project (webapi)](#api-project-webapi)
  - [Infrastructure Layer](#infrastructure-layer)
    - [Persistence Highlights](#persistence-highlights)
- [CQRS: Command Query Responsibility Segregation](#cqrs-command-query-responsibility-segregation)
- [Exception Handling](#exception-handling)
  - [Exception Handling vs. Error Handling](#exception-handling-vs-error-handling)
  - [Implementation](#implementation)
- [Comments DevOps \& Deployment](#comments-devops--deployment)
  - [token secret](#token-secret)
  - [ci/cd](#cicd)
- [Testing](#testing)
- [Fullstack Project](#fullstack-project)
  - [Features](#features)
    - [Mandatory features](#mandatory-features)
      - [User Functionalities](#user-functionalities)
      - [Admin Functionalities](#admin-functionalities)
    - [Bonus-point](#bonus-point)
  - [Requirements](#requirements)
  - [Getting Started](#getting-started)
  - [Testing](#testing-1)

# DB Schema / ERD

- ERD diagram
- Eraser comments

# Clean Architecture

# Domain-Driven Design

## Eventual Consistency; Aggregates as Transactional Boundaries

Ideally, each command updates a single aggregate in a single transaction.

Then, in an _"eventual consistent"_ manner, the other aggregates are updated via domain events.

# E-Commerce Project Architecture

## Domain Layer

### Rationale for strongly typed IDs as value objects

The chosen approach of using _strongly typed IDs_ for _entities_ in the domain model, and also _defining those IDs as value objects_ provides several benefits in the context of DDD:

#### Benefits of strongly typed IDs

- **Type safety** prevents accidental mixing of different but similar types of IDs.
  E.g., if IDs of two different entities were expected as arguments by a method, a type annotation of `(Guid idOfEntityA, Guid idOfEntityB)` would not prevent the potential mistake of IDs being passed in the wrong order.
- **Semantic meaning**: Code becomes more expressive and self-documenting.
- **Maintainability**: If the requirements for IDs change in the future, these updates won't modify the entity itself.
- **Separation of concerns**: The entity can focus on its core responsiblities and behavior, while the ID encapsulates the logic and rules specific to identifying the entity.

#### Benefits of defining strongly typed IDs as value objects

- **Encapsulation and validation**: By encapsulating IDs within a value object, validation rules and invariants specific to that ID type are easier to enforce.
- **Equality and comparison**: The `ValueObject` base class provides a consistent implementation of equality and comparison operators (`==`, `!=`, `GetHashCode`, `Equals`).
- **Immutability**: By inheriting from `ValueObject`, strongly typed IDs are immutable by default. This aligns with DDD, where value objects and entity IDs should be immutable to maintain a consistent state and avoid unintended side effects.
- **Testability**: By encapsulating ID logic in a separate value object, unit tests for the ID's behavior can easily be written without involving the entire entity.

### DDD base classes and interfaces

To maintain the integrity of the domain model in a Domain-Driven Design (DDD) context, to promote consistency of implementation and to reduce boilerplate code, the following base classes and interfaces have been implemented:

#### The `ValueObject` base class

[TODO: LINK to source code](www.example.com)

According to DDD principles, _value objects_ are immutable objects that represent a conceptual whole, and are distinguished by their value rather than their identity.

By implementing the `GetEqualityComponents()` method of the abstract `ValueObject`class, derived value object classes can define their criteria for equality based on their specific properties and fields.

The `ValueObject` class then implements the `Equals(T other)` method from the `IEquatable<T>` interface to define its own equality comparison logic:
It checks that the provided object is not null, is of the same type as the current instance, and then compares the equality components of both objects using the `SequenceEqual()` method.

The `Equals()` method is then used internally by the overloaded `==` and `!=` operators.

#### The `Entity` base class

[TODO: LINK to source code](www.example.com)
According to DDD principles, _entities_ are mutable objects and their (in)equality comparison is based only on their unique `id` value, regardless of their other properties.

#### The `AggregateRoot` base class

[TODO: LINK to source code](www.example.com)
The `AggregateRoot<TId, TIdType>` class extends the `Entity<TId>` and represents an aggregate root, which is the primary entity in a domain model.

#### The `AggregateRootId` and `EntityId` base classes

[TODO: LINK to source code](www.example.com)

Why separate definition of AggregateRootId vs. EntityId ???

The `EntityId<TId>` class is an abstract base class for strongly-typed identities representing the unique identifier of an entity. It extends the abstract `ValueObject` class.

The `AggregateRootId<TId>` class extends `EntityId<TId>` (and thereby also `ValueObject`) and represents the unique identifier of an aggregate root.

In the `AggregateRootId<TId>` class, `base(value)` is used in the constructor to initialize the `Value` property in the `EntityId<TId>` class.

`AggregateRootId<TId>` needs to initialize the `Value` property which is defined in its base class `EntityId<TId>`, so it calls the base class constructor to do this.

#### The `IDomainEvent` and `IHasDomainEvent` interfaces

[TODO: LINK to source code](www.example.com)

- 12, 13, 16, 17
- Explain rationale for strongly typed IDs, IDs as "value objects"
- DDD, Aggregates

- Domain Modeling
- TO DO: Miro Screenshot of Domain Modeling result
- LINK TO: DomainModels folder in Docs

- ?(Domain Events: explain storming process, link to docs, result, process modeling in Wiki?)

  - v=7LFxWgfJEeI
  - Part 10
  - Aggregates as transactional boundaries / Part 17 (0:00 - ) / side effects in one transaction (9:30)

- TO DO: Expand domain model classes with events

  - Part 17; v=MhoFCy_2-wQ

- Present and explain Domain proj folder structure
  - part 13 2-3:30

## Application Layer

Lorem ipsum

## Presentation Layer

Lorem ipsum

### Contracts Project (classlib)

The Contracts project is only referenced by the Api project, and its purpose is "documentation as code". It models the shape of Rest API requests and responses. These objects are then referenced inside the Controllers in the Api, which keeps the controller code concise and human-readable.

### Api Project (webapi)

## Infrastructure Layer

### Persistence Highlights

**PublishDomainEventsInterceptor**

- Part 17

# CQRS: Command Query Responsibility Segregation

- commands vs. queries
- use repositories only for commands >> data manipulation on the aggregate
- queries: no repositories. complex queries unrestricted by aggregates' transactional boundaries
- performance optimization on query side / query caching / ISP

# Exception Handling

goals

- privacy/security (responses and logging)
- performance
- monitoring: clear distinction between expected and unexpected exceptions
- maintainability / scalability:
  - track error flow? exceptions dontt only occur in controller, every component downstream needs to be able to handle and return meaningful error repsonse
  - new errors occur as scope and complexity of system grows, new features are added, etc.
- support for returning list of errors >> result pattern instead of binary decision: result object = discriminated union that either returns expected result or list of errors

## Exception Handling vs. Error Handling

exceptions = complicated are just go-to statements
performance: throwing exceptions = expensive
security: avoid passing exception details, stack trace, and information that allows to deduct how backend is implemented

## Implementation

![Static Badge](https://img.shields.io/badge/ErrorOr-v.2.0.1-black)

Registering as the very first middleware a `.UseExceptionHandler("/error")` encapsulates all following middleware in a try-catch-like pattern that re-routes and re-executes any request that throws an exception.

Subsequently, a failing request resets in a dedicated `ErrorsController` that extends `Problems()` from the `ControllerBase` class so that - _unless_ the exception can be matched to a custom domain error - a generic `500 Internal Server Error` without any sensitive information is returned to the user.

To achieve this separation, all controllers (_**except**_ the `ErrorsController`) are implemented so that their `Problems()` method accepts a list of custom errors (instead of receiving .NET's `ProblemDetails`), and then maps these custom error details to a meaningful `IActionResult` response.

The `CoreNutritionProblemDetailsFactory` is responsible for creating `ProblemDetails` and `ValidationProblemDetails` objects, which are used to provide detailed error information in an HTTP response.
Http status code only set at outermost layer following CA.
Entire error handling system speaks an expressive “internal language” of system.
It doesn’t have to “speak http”.

Custom error handling is implemented with help of the [ErrorOr](https://www.nuget.org/packages/ErrorOr) package, which...

error object:

```csharp
public struct Error : IEquatable<Error>
(
  public string Code { get; }
  public string Description { get; }
  public ErrorType Type { get; } // enum of methods: Failure(), Unexpected(), Validation(), Conflict(), NotFound()
)
```

Defining a domain error:

```csharp
// ../src/Domain/Common/Errors/Errors.User.cs
public static partial class Errors
{
  public static class User
  {
    public static Error DuplicateEmail()
    {
      Error.Conflict(
        code: "User.DuplicateEmail",
        description: "This email is already in use."
      );
    }
  }
}
```

results pattern + domain errors
domain errors can be centrally maintained, and because in Domain, referenced, understood and properly handled by any component in the entire system.

This implementation allows for any single component to return a (success) response, a single meaningful error, a list of meaningful errors, all while keeping the component code very concise.

Validation errors: 17:00 v=FXP3PQ03fa0

error handler will

- catch exception
- log it
- re-routes request to "/error" endpoint
- re-execute request

(request will not be re-executed if response already started)

- custom problem details factory

content-type:application/problem+json
type: link to RFC specificationo?
traceId:

# Comments DevOps & Deployment

## token secret

in .Api project in Development env --> JwtSettings:Secret in dotnet secret store

- dotnet user-secrets init
- dotnet user-secrets set "JwtSettings:Secret" "1-2-3-super-duper-mega-ultra-secret-key"
- dotnet user-secrets list

## ci/cd

CI/CD pipeline with gh actions > Azure

Publish:

Build and test:

# Testing

xunit
Moq
FluentAssertions

Testing Project structure

# Fullstack Project

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.8-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.8-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.16-drakblue)

This project involves creating a Fullstack project with React and Redux in the frontend and ASP.NET Core 7 in the backend. The goal is to provide a seamless experience for users, along with robust management system for administrators.

- Frontend: SASS, TypeScript, React, Redux Toolkit
- Backend: ASP.NET Core, Entity Framework Core, PostgreSQL

## Features

### Mandatory features

#### User Functionalities

1. User Management: Users should be able to register for an account and manage their profile.
2. Browse Products: Users should be able to view all available products and single product, search and sort products.
3. Add to Cart: Users should be able to add products to a shopping cart, and manage cart.
4. Oders: Users should be able to place orders and see the history of their orders.

#### Admin Functionalities

1. User Management: Admins should be able to manage all users.
2. Product Management: Admins should be able to manage all products.
3. Order Management: Admins should be able to manage all orders.

### Bonus-point

1. Third party integrations, for example: Google Authentication, Sending Email, Payment gateway, etc.
2. Extra features, for examples: dynamic pricing algorithms, chatbots, subscription, admin dashboard with analytics, etc.

## Requirements

1. Project should use CLEAN architecture, proper naming convention, security, and comply with Rest API. In README file, explain the structure of your project as well.
2. Error handler: This will ensure any exceptions thrown in your application are handled appropriately and helpful error messages are returned.
3. In backend server, unit testing (xunit) should be done, at least for Service(Use case) layer. We recommend to test entities, repositories and controllers as well.
4. Document with Swagger: Make sure to annotate your API endpoints and generate a Swagger UI for easier testing and documentation.
5. `README` file should sufficiently describe the project, as well as the deployment, link to frontend github.
6. Frontend, backend, and database servers need to be available in the live servers.

## Getting Started

1. Start with backend first before moving to frontend.
2. In the backend, here is the recommended order:

   - Plan Your Database Schema before start coding

   - Set Up the Project Structure

   - Build the models

   - Create the Repositories

   - Build the Services

   - Set Up Authentication & Authorization

   - Build the Controllers

   - Implement Error Handling Middleware

3. You should focus on the mandatory features first. Make sure you have minimal working project before opting for advanced functionalities.

Testing should be done along the development circle, early and regularly.

## Testing

Unit testing, and optionally integration testing, must be included for both frontend and backend code. Aim for high test coverage and ensure all major functionalities are covered.
