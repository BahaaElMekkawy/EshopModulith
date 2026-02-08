# EShop Modulith & Microservices 🛒

This project was built to deepen my expertise in modern software architecture and distributed systems.
Throughout the development process, I implemented advanced architectural patterns to address real-world scalability and reliability challenges, evolving the system from a Modular Monolith into a Microservices-based ecosystem.

## 🛠️ Tech Stack

**Frameworks / Languages:** .NET 10, C# 14, ASP.NET Core Minimal APIs, Carter  
**Databases / Caching:** PostgreSQL, Redis Distributed Cache  
**Messaging / Integration:** RabbitMQ, MassTransit, Outbox Pattern  
**Authentication / Identity:** Keycloak, OAuth2, OpenID Connect, JWT Bearer  
**Logging / Monitoring:** Serilog, Seq, Health Checks  
**Architecture / Patterns:** Modular Monolith, Vertical Slice Architecture (VSA), Domain-Driven Design (DDD), CQRS, Clean Architecture  
**Containerization / DevOps:** Docker, Docker Compose  

## 🏗️ Architectural Patterns Implemented

**Modular Monoliths (Modulith)**  
Designed for high cohesion with clear boundaries between business domains.

**Vertical Slice Architecture (VSA)**  
Organized code by features rather than layers to minimize coupling and improve maintainability.

**Domain-Driven Design (DDD)**  
Modeled complex business logic using Entities, Aggregates, and Value Objects.

**CQRS**  
Separated read and write concerns using the MediatR library.

**Outbox Pattern**  
Ensured reliable messaging and eventual consistency across modules and services.

## 📦 Module Breakdown

### 📘 Catalog Module
- Built with ASP.NET Core Minimal APIs using .NET 8 and C# 12
- Implemented Vertical Slice Architecture using feature folders and consolidated class files
- Configured CQRS validation pipeline behaviors with MediatR and FluentValidation
- Used Entity Framework Core (Code-First) with PostgreSQL
- Integrated Carter for clean and expressive Minimal API endpoint definitions
- Handled cross-cutting concerns:
  - Serilog / Seq logging
  - Global exception handling
  - Health checks

### 🛒 Basket Module
- Integrated Redis as a distributed cache on top of PostgreSQL
- Applied structural design patterns:
  - Proxy
  - Decorator
  - Cache-aside
- Developed asynchronous event publishing to RabbitMQ using MassTransit
- Implemented the Outbox Pattern to guarantee reliable messaging during the BasketCheckout use case

### 🔄 Module Communication

**Synchronous Communication**  
In-process method calls via public APIs between Catalog and Basket modules

**Asynchronous Communication**  
Price updates orchestrated using RabbitMQ and MassTransit

### 🔐 Identity & Security
- Orchestrated OAuth2 and OpenID Connect authentication flows using Keycloak
- Configured Keycloak inside Docker Compose as a standalone backing service
- Secured inter-module and external communications using JWT Bearer tokens

### 📦 Ordering Module
- Applied Clean Architecture principles alongside DDD and CQRS
- Ensured data integrity and consistency using the Outbox Pattern for checkout workflows

## 🚀 Road to Microservices

This project demonstrates a controlled migration strategy from modular monolith to microservices using the Strangler Fig Pattern.

This approach allows:
- Incremental service extraction
- Zero downtime evolution
- Preservation of system availability while modernizing the architecture

## 🎯 Project Goals
- Practice enterprise-grade backend architecture
- Implement reliable messaging and distributed system patterns
- Demonstrate real-world system evolution strategies
- Serve as a strong portfolio project for backend and full-stack roles
