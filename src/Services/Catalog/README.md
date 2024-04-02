# Product service

This service is responsible for managing products and categories in the catalog. Uses PostgreSQL as a write database and MongoDB for read database. When a product is created, updated or deleted, the service creates a event for consumers using MassTransit and RabbitMQ that triggered in SaveChangesAsync interceptor. After that, the service updates the read database with the new data.

## Port

The service runs on port 5051.