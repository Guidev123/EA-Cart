<p align="center">
  <a href="https://dotnet.microsoft.com/" target="blank"><img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" /></a>
</p>

   <h1>Cart API</h1>
    <p>This repository is part of the EA (Ecommerce Architecture) microservices, an Ecommerce project focused on clean architecture. The main goal is to serve as a model for organized and scalable architecture, consisting of three main layers and an API.</p>

  <h2>Project Structure</h2>
    <h3>1. <strong>Cart.API</strong></h3>
    <p>This is the entry point of the application. Here, API endpoints and configurations are defined.</p>

   <h3>2. <strong>Cart.Application</strong></h3>
    <p>Responsible for application logic, this layer contains:</p>
    <ul>
        <li><strong>UseCases:</strong> Use cases such as adding items to the cart, applying vouchers, etc.</li>
        <li><strong>Infrastructure service interfaces:</strong> Contracts for external or infrastructure services.</li>
        <li><strong>Resources related to application logic:</strong> Validations, mappings, and handlers.</li>
    </ul>

   <h3>3. <strong>Cart.Core</strong></h3>
    <p>Represents the domain layer, containing:</p>
    <ul>
        <li><strong>Entities and aggregates:</strong> The core representation of the domain.</li>
        <li><strong>Domain objects and enums:</strong> Business-related values and types.</li>
        <li><strong>Repository interfaces:</strong> Contracts for database operations.</li>
        <li><strong>Business rules:</strong> Rich entities with well-defined logic and behavior, following Domain-Driven Design (DDD) principles.</li>
    </ul>

   <h3>4. <strong>Cart.Infrastructure</strong></h3>
    <p>Contains implementations related to application infrastructure, including:</p>
    <ul>
        <li><strong>Repositories:</strong> Implementation of interfaces defined in the domain.</li>
        <li><strong>Migrations and database configuration:</strong> Management of schema and data persistence.</li>
        <li><strong>Mappings:</strong> Relationships between entities and database tables.</li>
        <li><strong>Infrastructure services:</strong> Implementation of external services and low-level operations.</li>
    </ul>

  <h2>UseCase Structure</h2>
    <p>Below is the tree structure of the use cases located in the <code>Cart.Application</code> layer:</p>
   <pre>
UseCases/
├── Cart/
│   ├── AddItem/
│   │   ├── AddItemToCartHandler.cs
│   │   ├── AddItemToCartRequest.cs
│   │   └── AddItemToCartResponse.cs
│   ├── ApplyVoucher/
│   │   ├── ApplyVoucherToCartHandler.cs
│   │   ├── ApplyVoucherToCartRequest.cs
│   │   └── ApplyVoucherToCartResponse.cs
├── Item/
│   ├── Remove/
│   │   ├── RemoveItemFromCartHandler.cs
│   │   ├── RemoveItemFromCartRequest.cs
│   │   └── RemoveItemFromCartResponse.cs
│   ├── Update/
│   │   ├── UpdateCartItemHandler.cs
│   │   ├── UpdateCartItemRequest.cs
│   │   └── UpdateCartItemResponse.cs
├── Voucher/
│   ├── Create/
│   │   ├── CreateVoucherHandler.cs
│   │   ├── CreateVoucherRequest.cs
│   │   └── CreateVoucherResponse.cs
├── Handler.cs
└── IUseCase.cs
    </pre>





