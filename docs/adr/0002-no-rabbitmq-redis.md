# ADR-0002: No RabbitMQ or Redis in Initial Architecture

## Status
Accepted

## Context

ForgeERP is designed as a modular monolith ERP system developed by a single developer.

The application modules:
- Catalog
- Inventory
- Sales
- Manufacturing
- Planning

all run inside the same process and share the same database.

During architecture planning, a decision was required on whether to include:
- A message broker such as :contentReference[oaicite:0]{index=0}
- A distributed cache such as :contentReference[oaicite:1]{index=1}

These technologies are commonly used in large-scale distributed systems and microservice architectures.

The question was whether ForgeERP currently requires asynchronous distributed messaging or distributed caching infrastructure.

## Decision

ForgeERP will not use RabbitMQ, Redis, or similar infrastructure components in the initial architecture.

The system will instead use:
- Direct in-process communication between modules
- Database persistence through a relational database
- In-memory application caching only where necessary
- Synchronous transactions for business workflows

Domain events may still exist conceptually inside the application, but they will be handled internally within the modular monolith rather than through an external message broker.

## Consequences

### Benefits

- Simpler architecture
  - Fewer moving parts and infrastructure dependencies

- Easier local development
  - Developers do not need to install and manage additional services

- Easier debugging
  - Requests remain inside a single process without asynchronous distributed tracing complexity

- Lower operational overhead
  - No broker clusters, cache servers, monitoring pipelines, or synchronization issues

- Better consistency
  - Business workflows can use immediate transactional consistency instead of eventual consistency

- Appropriate for current scale
  - The application does not currently have high-throughput or distributed deployment requirements

### Why RabbitMQ Was Not Chosen

Message brokers are valuable when:
- Services are independently deployed
- Systems require asynchronous processing
- Workloads must be distributed across many services
- Temporary service failures must be isolated through queues

ForgeERP currently does not have these requirements because:
- Modules are inside the same application
- Communication is internal and synchronous
- There is no distributed deployment topology
- Transaction coordination is simpler with direct calls

Using RabbitMQ at this stage would introduce:
- Queue management complexity
- Retry and idempotency concerns
- Eventual consistency challenges
- Additional infrastructure maintenance

without sufficient architectural benefit.

### Why Redis Was Not Chosen

Distributed caching systems are valuable when:
- Applications run across multiple servers
- Database load becomes a major bottleneck
- Extremely low-latency reads are required
- Shared distributed sessions are necessary

ForgeERP currently does not have these requirements because:
- The system is expected to run on a single application instance initially
- ERP workloads are primarily transactional rather than ultra-high throughput
- Database performance requirements are manageable with proper indexing and query design

Using Redis at this stage would introduce:
- Cache invalidation complexity
- Data synchronization concerns
- Additional deployment infrastructure
- More operational monitoring requirements

without solving a current bottleneck.

## Conclusion

Excluding RabbitMQ and Redis keeps ForgeERP focused on simplicity, maintainability, and delivery speed.

The architecture avoids premature optimization and unnecessary distributed-system complexity while remaining open to future evolution if scaling requirements change later.