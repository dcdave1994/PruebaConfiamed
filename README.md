# Prueba Técnica Confiamed

## Tecnologías utilizadas

- ASP.NET Core 10
- Entity Framework Core
- SQL Server
- Swagger
- Arquitectura por capas

## Arquitectura

- Controllers
- Services
- Repositories
- Entities
- DTOs

## Base de datos

La base de datos se genera mediante Entity Framework Core utilizando migraciones.

Ejecutar:

```bash
Update-Database
```

## Endpoints

### Usuarios

GET /api/users

GET /api/users/{id}

POST /api/users

### WorkItems

GET /api/workitems

GET /api/workitems/{id}

POST /api/workitems

## Reglas implementadas

- Asignación automática de WorkItems.
- Prioridad alta y baja.
- Distribución por menor carga de trabajo.
- Exclusión de usuarios saturados.
- Orden de pendientes por prioridad y fecha.
