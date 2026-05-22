# ADR-0001: Modular Monolith Instead of Microservices

## Status
Accepted

## Context

ForgeERP is a manufacturing ERP system containing multiple business domains such as Catalog, Inventory, Sales, Manufacturing, and Planning.

These modules are closely connected and frequently share data and business workflows. For example:

- Sales depends on Inventory for stock availability
- Manufacturing depends on Catalog for BOMs and Routings
- Planning depends on Sales, Inventory, and Manufacturing data for MRP calculations

The project is currently developed by a single developer and targets a small-to-medium scale deployment scenario.

A decision was required between:
- Building the system as microservices
- Building the system as a modular monolith

Modern ERP systems such as :contentReference[oaicite:0]{index=0} and :contentReference[oaicite:1]{index=1} commonly use strongly modular architectures with clear boundaries, while still avoiding unnecessary operational complexity for tightly coupled business domains.

## Decision

ForgeERP will use a modular monolith architecture.

Each bounded context will be implemented as an isolated module inside a single deployable application:
- Catalog
- Inventory
- Sales
- Manufacturing
- Planning

The system will enforce clean boundaries between modules through:
- Separate application layers
- Separate domain models
- Explicit contracts between modules
- Independent namespaces and folders

Modules may communicate internally through application services, events, or interfaces while sharing the same runtime and database infrastructure.

## Consequences

### Benefits

- Simpler deployment and infrastructure
  - Only one application and one database need to be deployed and maintained

- Easier development for a solo developer
  - No distributed system complexity
  - Faster local development and debugging

- Easier transactional consistency
  - Business operations across modules can use shared database transactions

- Lower operational overhead
  - No service discovery, API gateway, distributed tracing, or inter-service messaging infrastructure required

- Clear domain boundaries still exist
  - The modular structure supports future extraction into microservices if scaling requirements change

- Better fit for ERP systems
  - ERP modules are naturally highly connected and frequently share workflows and data

### Tradeoffs

- Modules cannot be deployed independently
- Scaling happens at the application level rather than per service
- Poor architectural discipline could eventually lead to tight coupling between modules
- Future migration to microservices would require additional refactoring if system scale grows significantly

## Conclusion

A modular monolith provides the best balance between maintainability, architectural clarity, and delivery speed for ForgeERP at its current scale and team size.

The architecture preserves strong domain boundaries without introducing the operational and cognitive complexity of microservices too early.