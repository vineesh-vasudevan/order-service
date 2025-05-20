# 🛒 Ordering Microservice

A robust Ordering Microservice built with **Domain-Driven Design (DDD)**, **CQRS**, and **Clean Architecture** principles. Designed for scalability, maintainability, and separation of concerns using modern .NET 8 features, MediatR, EF Core, and RabbitMQ with MassTransit.

---

## 📐 Architecture Overview

This microservice follows the Clean Architecture layered design:

- **Domain Layer**: Core business logic using DDD tactical patterns.
- **Application Layer**: Use case orchestration using CQRS with MediatR.
- **Infrastructure Layer**: Database access using EF Core 8, integration services.
- **API Layer**: Exposes minimal REST endpoints via Carter.
- **Contracts Layer**: Defines data transfer objects (DTOs) for communication between services or layers
- **Shared Layer**: General utilities and shared dependencies used by multiple layers.: - Behaviors, CORS, Exceptions

---

## 🚀 Tech Stack

### 🧱 Core Framework & Language

- **.NET 8** with **C# 12**
- **Minimal APIs** using [Carter](https://github.com/CarterCommunity/Carter) for modular, clean endpoint definitions

### 📦 Architectural Patterns & Design

- **Domain-Driven Design (DDD)** with:
  - Entities, Value Objects, Aggregates
  - Domain Events vs Integration Events
  - Strongly Typed IDs
- **CQRS (Command Query Responsibility Segregation)** with [MediatR](https://github.com/jbogard/MediatR)
- **MediatR Pipeline Behaviors** for:
  - **Validation** with [FluentValidation](https://fluentvalidation.net/)
  - **Logging** and cross-cutting concerns

### 🔄 Mapping & Functional Helpers

- **AutoMapper** for clean object-to-object mapping
- **CSharpFunctionalExtensions** for functional programming constructs like `Result`, `Maybe`, and value semantics
- **Ardalis.SmartEnum** for type-safe, behavior-rich enums

### 🗃️ Data & Persistence

- **Entity Framework Core 8** (Code-First, Migrations, Value Object Mapping)
- **SQL Server** as the primary relational database
- **ModelBuilder Entity Configurations**
- **Code-First Migrations** and Value Object Mapping
- **SaveChanges Interceptors** to handle cross-cutting concerns (e.g., auditing, soft deletes)
- **Automatic auditing**: setting `CreatedAt`, `ModifiedAt`, `CreatedBy`, etc.
- **Domain Event Dispatching** integrated with EF Core , MediatR and Unit of Work
- **Auto Migrate & Seed** at application startup

### 📡 Messaging & Async Communication

- **MassTransit** as a messaging abstraction
- **RabbitMQ** as the message broker
- Event-driven communication via **Publish/Subscribe** and **Topic Exchanges**

### 📋 Monitoring & Observability

- **Health Checks** with `AspNetCore.Diagnostics.HealthChecks` and `HealthChecks.UI.Client`
- **Correlation ID Generator** for distributed tracing
- **Serilog** for structured logging and diagnostics

### ⚙️ Cross-Cutting Concerns

- **Global Exception Handling Middleware**
- **Centralized Logging & Monitoring**
- **Health Monitoring Endpoints**

### 🐳 DevOps & Deployment

- **Docker** for containerizing the Ordering Microservice
- **Docker Compose** for orchestrating the app with SQL Server and RabbitMQ

## 📦 Migrations

### 🔧 Create a Migration

```bash

dotnet ef migrations add InitialCreate --project .\src\OrderService.Infrastructure --startup-project .\src\OrderService.Api --context OrderDbContext --output-dir Data\Migrations
```

### 🚀 Apply the Migration

```bash
dotnet ef database update --project .\src\OrderService.Infrastructure --startup-project .\src\OrderService.Api --context OrderDbContext
```

✅ You can skip this step if you're just running the app — it will apply the latest migrations automatically.

## 📈 Planned Improvements

- 🔐 **Authentication & Authorization**

  - Integrate JWT or API key-based security for gRPC endpoints to protect the service.

- 🔁 **Retry & Circuit Breaker Policies**

  - Implement Retry and Circuit Breaker policies with Polly

- 🗃️ **Unit & Integration Tests improvements**

  - Increase Unit Testing & Integration Testing Coverage

- 📤 Outbox Pattern

  - Implement the Outbox Pattern using EF Core and MassTransit to ensure reliable event publishing:
