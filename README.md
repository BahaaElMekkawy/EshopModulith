EShop Modulith & Microservices 🛒
I built this project to deepen my expertise in modern software architecture and distributed systems. Throughout the development process, I have implemented advanced patterns to solve real-world scalability and reliability challenges, transitioning from a Modular Monolith to a Microservices ecosystem.

🏗️ Architectural Patterns I Have Implemented
Modular Monoliths (Modulith): Designed for high cohesion and clear boundaries between business domains.

Vertical Slice Architecture (VSA): Organized code by features rather than layers to minimize coupling.

Domain-Driven Design (DDD): Modeled complex logic using Entities, Aggregates, and Value Objects.

CQRS: Separated read and write concerns using the MediatR library.

Outbox Pattern: Guaranteed reliable messaging and eventual consistency across services.

📦 Module Breakdown
Catalog Module
Utilized ASP.NET Core Minimal APIs with .NET 8 and C# 12 features.

Implemented Vertical Slice Architecture using feature folders and consolidated class files.

Configured CQRS Validation Pipeline Behaviors with MediatR and FluentValidation.

Leveraged Entity Framework Core (Code-First) with PostgreSQL.

Integrated Carter for elegant Minimal API endpoint definitions.

Handled cross-cutting concerns: Serilog/Seq Logging, Global Exception Handling, and Health Checks.

Basket Module
Integrated Redis as a Distributed Cache over a PostgreSQL database.

Applied structural patterns: Proxy, Decorator, and Cache-aside.

Developed asynchronous event publishing to RabbitMQ via MassTransit.

Implemented the Outbox Pattern for reliable messaging during the BasketCheckout use case.

Module Communications
Sync: Facilitated in-process method calls (Public APIs) between Catalog and Basket.

Async: Orchestrated price updates between modules using RabbitMQ & MassTransit.

Identity & Security
Orchestrated OAuth2 + OpenID Connect flows using Keycloak.

Configured Keycloak within Docker Compose as a standalone backing service.

Secured communications using JwtBearer tokens.

Ordering Module
Applied Clean Architecture principles alongside DDD and CQRS.

Ensured data integrity with the Outbox Pattern for checkout workflows.

🚀 The Road to Microservices
I have implemented a migration path from EShop Modules to Microservices using the Strangler Fig Pattern. This demonstrates my ability to evolve legacy systems into modern, distributed architectures without compromising system availability.
