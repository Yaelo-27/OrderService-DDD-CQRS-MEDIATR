# Clean Architecture API with DDD AND CQRS

Pequeña API REST desarrollada con .NET 9, implementando **Clean Architecture** con capas Domain, Application, Infrastructure e API. Incluye MediatR para CQRS, Entity Framework Core para persistencia, y soporte para Docker.

## Características

- ✅ **Clean Architecture** con CQRS (Commands & Queries) separación lógica
- ✅ **MediatR** para manejo de comandos y consultas
- ✅ **Entity Framework Core** con SQL Server
- ✅ **Minimal APIs** (.NET 9)
- ✅ **Integration Tests** con xUnit
- ✅ **Docker & Docker Compose** para fácil deployment

## Estructura del Proyecto

```
ApiRedis/
├── Api/                          # Capa Presentación (Minimal APIs)
│   ├── Program.cs               # Configuración aplicación
│   ├── appsettings.json         # Configuración local
│   ├── Dockerfile               # Imagen Docker
│   ├── Api.http                 # Ejemplos HTTP
│   └── DI/                       # Inyección de dependencias
│
├── Aplication/                   # Capa Aplicación (CQRS)
│   ├── Orders/
│   │   ├── CreateOrder/         # Command para crear orders
│   │   └── GetOrder/            # Query para obtener orders
│   ├── dto/                     # Data Transfer Objects
│   └── DI/                      # Registro de MediatR
│
├── Infrastructure/              # Capa Infraestructura (Persistencia)
│   ├── Persistence/
│   │   ├── OrderDbContext.cs    # DbContext
│   │   ├── Configuration/       # EF Core Configurations
│   │   ├── Migrations/          # EF Core Migrations
│   │   └── Repositories/
│   └── DI/                      # Registro de BD
│
├── Domain/                       # Capa Dominio (Lógica de negocio)
│   ├── Orders/
│   │   ├── Order.cs             # Entidad agregada
│   │   ├── OrderId.cs           # Value Object
│   │   └── IOrderRepository.cs  # Interfaz repositorio
│   ├── ValueObjects/            # Address, Contact, etc.
│   └── Primitives/              # AggregateRoot, DomainEvent
│
├── Api.IntegrationTests/        # Tests de integración
│   ├── OrdersIntegrationTests.cs
│   └── TestApiFactory.cs        # Configuración para tests
│
├── docker-compose.yml           # Orquestación de contenedores
├── init-db.sql                  # Script de inicialización BD
└── ApiRedis.slnx               # Solution file
```

## Inicio Rápido

### Requisitos

- .NET 9.0 SDK
- Docker & Docker Compose (para deployment containerizado)
- SQL Server 2025 (local o en contenedor)

### Instalación Local

1. **Clonar y restaurar paquetes:**
   ```powershell
   git clone <repository-url>
   cd ApiRedis
   dotnet restore
   ```

2. **Configurar base de datos en `appsettings.json`:**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=OrderDb;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=true;TrustServerCertificate=true;"
     }
   }
   ```

3. **Crear migraciones y aplicar (opcional):**
   ```powershell
   dotnet ef migrations add InitialCreate --project Infrastructure --startup-project Api
   dotnet ef database update --project Infrastructure --startup-project Api
   ```

4. **Ejecutar la API:**
   ```powershell
   dotnet run --project Api/Api.csproj
   ```

   La API escuchará en `https://localhost:5001`.

### Instalación con Docker

1. **Crear volumen:**
   ```powershell
   docker volume create sql-vol
   ```

2. **Levantar servicios:**
   ```powershell
   docker compose up -d --build
   ```

   - **API:** http://localhost:5150
   - **SQL Server:** Interno en contenedor (no expuesto)

3. **Ver logs:**
   ```powershell
   docker compose logs -f api
   docker compose logs -f db
   ```

4. **Parar servicios:**
   ```powershell
   docker compose down
   ```

## 📡 Endpoints

### POST /orders - Crear Pedido

**Request:**
```http
POST http://localhost:5150/orders
Content-Type: application/json

{
  "ShippingContactName": "John Doe",
  "ShippingContactEmail": "john@example.com",
  "ShippingContactPhoneNumber": "+34123456789",
  "ShippingAddressStreet": "123 Main St",
  "ShippingAddressCity": "Madrid",
  "ShippingAddressState": "MA",
  "ShippingAddressPostalCode": "28001",
  "ShippingAddressCountry": "Spain",
  "Items": [
    {
      "ProductId": 1,
      "ProductName": "Laptop",
      "Quantity": 1,
      "UnitPrice": 1299.99
    }
  ]
}
```

**Response:** `201 Created`
```
Location: /orders/{orderId}
Body: {orderId}
```

### GET /orders/{id} - Obtener Pedido

**Request:**
```http
GET http://localhost:5150/orders/550e8400-e29b-41d4-a716-446655440000
Accept: application/json
```

**Response:** `200 OK`
```json
{
  "ShippingContactName": "John Doe",
  "ShippingContactEmail": "john@example.com",
  "ShippingContactPhone": "+34123456789",
  "ShippingAddressStreet": "123 Main St",
  "ShippingAddressCity": "Madrid",
  "ShippingAddressState": "MA",
  "ShippingAddressPostalCode": "28001",
  "Status": "Pending",
  "Items": [
    {
      "ProductId": 1,
      "ProductName": "Laptop",
      "Quantity": 1,
      "UnitPrice": 1299.99
    }
  ]
}
```

## 🧪 Testing

### Ejecutar Tests de Integración

```powershell
dotnet test Api.IntegrationTests
```

Los tests utilizan la BD configurada en `appsettings.json`. Verifican:
- POST /orders retorna `201 Created`
- GET /orders/{id} retorna `200 OK` con los datos del pedido

## Configuración

### Variables de Entorno (Docker)

En `docker-compose.yml`:

```yaml
environment:
  - ASPNETCORE_ENVIRONMENT=Production
  - ConnectionStrings__DefaultConnection=Server=db,1433;Database=OrderDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;
  - MSSQL_SA_PASSWORD=YourStrong!Passw0rd
```

**Cambiar contraseña:**
1. Actualizar `MSSQL_SA_PASSWORD` en servicio `db`
2. Actualizar `ConnectionStrings__DefaultConnection` en servicio `api` con la misma contraseña

### Archivo de Configuración Local

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=OrderDb;User Id=sa;Password=YourStrong!Passw0rd;Encrypt=true;TrustServerCertificate=true;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

## Dependencias Principales

- **MediatR** (v14.0.0) - CQRS pattern
- **Entity Framework Core** (v8.0.4) - ORM
- **FluentValidation** (v11.8.1) - Validación de datos
- **xUnit** (v2.4.2) - Testing framework

## Patrones Implementados

### Clean Architecture

- **Domain:** Reglas de negocio independientes
- **Application:** Lógica de aplicación (Commands/Queries)
- **Infrastructure:** Detalles técnicos (BD, repositorios)
- **API:** Presentación HTTP

### CQRS (Command Query Responsibility Segregation)

- **Commands:** `CreateOrderCommand` - modifica estado
- **Queries:** `GetOrderQuery` - solo lectura

### Repository Pattern

Abstracción de acceso a datos a través de `IOrderRepository`.

### Dependency Injection

Configuración centralizada en las clases `DependencyInjection` de cada capa. A través de assembly

## Convenciones

- **Nombres de espacios:** `[Capa].[Funcionalidad]`
- **Migrations:** Nombradas con timestamp y descripción
- **DTOs:** Sufijo `Dto` (ej: `OrderDto`)
- **Commands:** Sufijo `Command` (ej: `CreateOrderCommand`)
- **Queries:** Sufijo `Query` (ej: `GetOrderQuery`)

## 🐛 Troubleshooting

### Error: "Cannot find base definition for table X"
**Solución:** Ejecutar migraciones:
```powershell
dotnet ef database update --project Infrastructure --startup-project Api
```

### Error: "Connection to server 'db' failed"
**Solución (Docker):** Asegurar que el servicio `db` está healthy:
```powershell
docker compose logs db
docker compose up -d --build  # Re-levantar servicios
```

### Error: "Invalid phone number format"
**Solución:** El formato debe ser internacional: `+{countrycode}{number}` (ej: `+34123456789`)

## Referencias

- [Clean Architecture - Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Docker Compose](https://docs.docker.com/compose/)