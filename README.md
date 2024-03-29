# Microcommerce

Microcommerce is a microservices based e-commerce platform. It is built using mostly Dotnet technologies and is designed to be deployed on Kubernetes. Uses Kafka as the message broker and many different database technologies such as MongoDB, PostgreSQL, Redis to serve different services.

## Services

### API Gateway

The API Gateway is the entry point for all the requests. It is built using YARP and is responsible for routing the requests to the appropriate services. It also handles authentication and authorization with the help of Keycloak.

### Auth Server

The Auth Server is responsible for handling the authentication and authorization of the users. It is built using Keycloak and is responsible for generating the access tokens and refresh tokens. 

There is only two different roles for users, `customer` and `manager`. The `customer` role is the default role for all the users and the `manager` role is for the users who have the permission to manage the products and stocks. When a user registers, the user is assigned the `customer` role by default.

### Product Service 

The Product Service is responsible for handling all the product related operations. It is built using Dotnet and uses MongoDB as the database.

### Order Service

The Order Service is responsible for handling all the order related operations. It is built using Dotnet and uses PostgreSQL as the database.

### Basket Service

The Basket Service is responsible for handling all the basket related operations. It is built using Dotnet and uses Redis as the database.

### Stock Service

The Stock Service is responsible for handling all the stock related operations. It is built using Dotnet and uses PostgreSQL as the database.

### Notification Service

The Notification Service is responsible for handling all the notification related operations. It is built using Dotnet, connects to avaliable clients with SignalR and uses Kafka as the message broker.

### Saga Orchestrator Service

The Saga Orchestrator Service is responsible for handling all the saga related operations. It is built using Dotnet and MassTransit.




