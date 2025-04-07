# Inventory Management System

A robust inventory tracking solution built with ASP.NET Core and Entity Framework Core.

## Features

- Product catalog with categories
- Supplier management
- Inventory transactions (purchases/sales)
- Real-time stock level tracking
- CSV reporting

## Technologies

- .NET 8
- Entity Framework Core 8
- SQL Server
- xUnit (testing)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/atabekyan98/inventory-management-system
   ```

2. Configure the database connection in `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=InventoryDB;Trusted_Connection=True;"
   }
   ```

3. Apply database migrations:

   ```bash
   dotnet ef database update --project src/Inventory.Infrastructure
   ```

4. Run the application:

   ```bash
   dotnet run --project src/Inventory.API
   ```

## API Endpoints

| Method | Endpoint                        | Description             |
|--------|---------------------------------|-------------------------|
| GET    | /api/products                   | List all products       |
| POST   | /api/products                   | Create new product      |
| GET    | /api/products/{id}              | Get product details     |
| PUT    | /api/products/{id}              | Update product          |
| DELETE | /api/products/{id}              | Delete product          |
| POST   | /api/transactions/purchase      | Record inventory purchase |
| POST   | /api/transactions/sale          | Record inventory sale   |

### Testing

- Run unit tests:

  ```bash
  dotnet test tests/Inventory.Core.UnitTests
  ```

- Run integration tests:

  ```bash
  dotnet test tests/Inventory.API.IntegrationTests
  ```

### License
This project is licensed under the MIT License.
