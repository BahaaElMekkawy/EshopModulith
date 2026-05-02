# EShopModulith 🛒

EshopModulith is a .NET-based modular monolith designed to showcase how well-defined domain boundaries can be achieved within a single deployable system.

This project was created to strengthen my understanding of modern software architecture and distributed systems. During its development, I applied advanced architectural patterns to solve real-world challenges related to scalability, maintainability, and system reliability. Over time, the system evolved from a modular monolith into a microservices-oriented architecture, reflecting practical trade-offs between simplicity and distributed complexity.

The system is organized around three business modules:
- `Catalog`
- `Basket`
- `Ordering`

It combines in-process module communication with asynchronous integration events, and uses the outbox pattern to improve messaging reliability.

## Table of Contents

- [Architecture at a Glance](#architecture-at-a-glance)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Module Responsibilities](#module-responsibilities)
- [Communication and Messaging](#communication-and-messaging)
- [Data and Persistence](#data-and-persistence)
- [API Surface](#api-surface)
- [Security and Identity](#security-and-identity)
- [Cross-Cutting Concerns](#cross-cutting-concerns)
- [Local Infrastructure](#local-infrastructure)
- [Getting Started](#getting-started)
- [Current Gaps and Next Improvements](#current-gaps-and-next-improvements)

## Architecture at a Glance

- **Architectural style:** Modular Monolith (Modulith)
- **Internal organization:** Vertical Slice + CQRS
- **Domain modeling:** DDD-inspired aggregates, value objects, and domain events
- **Host composition:** Single API bootstrapper that wires all modules
- **Consistency strategy:** Synchronous in-process contracts + asynchronous integration events

The API host registers each module using explicit extension points (`AddXModule` / `UseXModule`), preserving a clear separation between domain areas while running as one process.

## Technology Stack

- **Runtime / language:** .NET 10, C#
- **API:** ASP.NET Core Minimal APIs, Carter
- **Application pipeline:** MediatR, FluentValidation
- **Data access:** Entity Framework Core, PostgreSQL (Npgsql)
- **Caching:** Redis distributed cache
- **Messaging:** MassTransit on RabbitMQ
- **Authentication:** Keycloak JWT Bearer integration
- **Observability:** Serilog + Seq
- **Containerization:** Docker, Docker Compose

## Project Structure

```text
src/
  Bootstrapper/
    EshopModulith.Api/                  # Main host and module composition
  Modules/
    Catalog/EshopModulith.Catalog/      # Catalog module
    Basket/EshopModulith.Basket/        # Basket module
    Ordering/EshopModulith.Ordering/    # Ordering module
  Shared/
    EshopModulith.Shared/               # Cross-cutting primitives and behaviors
    EshopModulith.Shared.Contracts/     # In-process contracts between modules
    EshopModulith.Shared.Messaging/     # Integration events and bus wiring
  docker-compose.yml
  docker-compose.override.yml
```
<img width="662" height="517" alt="Screenshot 2026-05-02 185723" src="https://github.com/user-attachments/assets/cbb930b6-681e-4cb5-aee9-cb3228f74ce8" />


## Module Responsibilities

### Catalog
- Manages product read and write use cases.
- Publishes integration events when product pricing changes.
- Owns a dedicated persistence schema (`catalog`).

### Basket
- Manages user baskets and basket item operations.
- Uses cache-aside behavior through repository decoration (`CachedBasketRepository`).
- Implements checkout outbox records for reliable event publishing.
- Owns a dedicated persistence schema (`basket`).

### Ordering
- Consumes basket checkout integration events.
- Creates and persists order aggregates.
- Owns a dedicated persistence schema (`ordering`).

## Communication and Messaging

### Synchronous communication
- Basket performs in-process product lookups through shared contracts and MediatR.
- Example flow: Basket add-item -> send `GetProductByIdQuery` -> Catalog handler resolves product.

### Asynchronous communication
- Implemented via MassTransit + RabbitMQ.
- Key integration event flows:
  - Product price changed -> Catalog publishes -> Basket consumes and updates basket item prices.
  - Basket checkout -> Basket stores outbox record -> background processor publishes -> Ordering consumes and creates orders.

### Outbox pattern
Basket checkout reliability uses an outbox workflow:
1. Write outbox record in the same transaction as business changes.
2. Poll unpublished outbox records in a background processor.
3. Publish to RabbitMQ.
4. Mark message as processed.

This prevents losing messages between database commits and broker publication.

## Data and Persistence

- One `DbContext` per module:
  - `CatalogDbContext`
  - `BasketDbContext`
  - `OrderingDbContext`
- Each module has its own migrations and schema.
- Startup applies migrations using shared migration extensions.
- Shared EF Core interceptors handle:
  - audit fields
  - domain event dispatch

## API Surface

Representative endpoint groups:
- **Catalog:** `/products`, `/products/{id:guid}`, `/products/category/{category}`
- **Basket:** `/basket`, `/basket/{userName}`, `/basket/{userName}/items`, `/basket/checkout`
- **Ordering:** `/orders`, `/orders/{id}`

Endpoints are implemented as feature-local Carter modules and typically dispatch requests through MediatR handlers.

## Security and Identity

- JWT authentication is integrated with Keycloak.
- Authorization services are registered at host level.
- Basket endpoints are explicitly protected with authorization requirements.

## Cross-Cutting Concerns

- **Logging:** Serilog request/application logging with Seq sink.
- **Validation:** FluentValidation via MediatR pipeline behavior.
- **Error handling:** centralized exception mapping to `ProblemDetails`.
- **Caching:** Redis-backed repository decorator in Basket.
- **Domain events:** dispatched through EF Core save interceptors.

## Local Infrastructure

Docker Compose provides development dependencies:
- PostgreSQL
- Redis
- RabbitMQ (with management UI)
- Keycloak
- Seq

Primary app configuration lives in:
- `src/Bootstrapper/EshopModulith.Api/appsettings.json`
- `src/Bootstrapper/EshopModulith.Api/appsettings.Development.json`

## Getting Started

1. Start infrastructure services with Docker Compose.
2. Run the API host project (`EshopModulith.Api`).
3. On startup, module migrations are applied and modules are wired.

## Next Improvements

- Add automated tests (unit, integration, and contract-level where needed).
- Add CI workflows for build/test and quality checks.


