# User API - Technical Test (.NET 10 + PostgreSQL)

API desarrollada en .NET 10 con arquitectura limpia, CQRS, validaciones, autenticación básica y acceso a datos mediante Stored Procedures en PostgreSQL.

---

## 🧱 Arquitectura

La solución está organizada en capas siguiendo Clean Architecture:

- **UserApi.Api** → Controllers, Middleware, Auth
- **UserApi.Application** → Commands, Handlers, DTOs, Interfaces, Validators
- **UserApi.Infrastructure** → Repositorios (acceso a BD por Stored Procedures)
- **UserApi.Domain** → Capa de dominio (sin entidades, ya que la lógica está en BD)
- **UserApi.Tests** → Pruebas unitarias (Handlers y Validators)

Patrones aplicados:
- CQRS
- Repository Pattern
- Dependency Injection
- Middleware de manejo de errores

---

## ️ Base de Datos (PostgreSQL)

### Requisitos
- PostgreSQL 14+

###  Scripts

Ejecutar en este orden:

1. `00_create_database.sql`
2. `01_create_tables.sql`
3. `02_seed_catalogs.sql`
4. `03_sp_create`


Las tablas incluyen:
- Country
- Department
- City
- AppUser

Con llaves foráneas correctamente definidas.

---

##  Configuración

Editar `appsettings.json` en **UserApi.Api**:

json
{
  "ConnectionStrings": {
    "Default": "Host=127.0.0.1;Port=5432;Database=UserDb;Username=postgres;Password=admin123"
  },
  "BasicAuth": {
    "Username": "admind",
    "Password": "12345"
  }
}


## Autenticación

La API usa Basic Authentication.

Credenciales por defecto:

Usuario: admind

Password: 12345

En Swagger:

Click en Authorize 🔒

Ingresar usuario y contraseña


🌐 Endpoints
📌 Usuarios

POST /api/users

Body:

{
  "fullName": "Juan Perez",
  "phone": "3001234567",
  "address": "Calle 123",
  "cityId": 1
}

📌 Catálogos (vía Stored Procedures)

GET /api/catalog/countries

GET /api/catalog/departments/{countryId}

GET /api/catalog/cities/{departmentId}

🧪 Pruebas Unitarias

Las pruebas están en el proyecto:

UserApi.Tests