
# 🧠 Caso Práctico 2 – CRUD/ORM con SQL Server y Sakila

**Maestría en Ciencia de Datos e Inteligencia Artificial (MACDIA)**  
**Materia:** Ciencia de Datos I – INF-7303-C1  
**Profesor:** Silverio Del Orbe A.  

---

## 🎯 Objetivo del proyecto

Este repositorio forma parte del segundo caso práctico colaborativo de la materia, en el cual se desarrolla un sistema CRUD/ORM sobre la base de datos Sakila (SQL Server), aplicando los principios de programación orientada a objetos, estructuras de datos y arquitectura escalable para el consumo estandarizado de datos.

---

## 🧱 Arquitectura de la solución

La solución está organizada siguiendo los principios de **Clean Architecture** y **CQRS (Command Query Responsibility Segregation)**, lo que permite un desacoplamiento claro entre la lógica de negocio, los contratos de comunicación y la infraestructura de persistencia.

### 🔹 `Sakila.API` – ASP.NET Core Web API
- Exposición de endpoints RESTful (`/api/languages`, etc.)
- Controladores minimalistas con `IMediator` (MediatR)
- Validaciones automáticas con `FluentValidation` vía `ValidationBehavior`
- Proyecciones eficientes de datos con `AutoMapper.ProjectTo<>`

### 🔹 `Sakila.Application` – Lógica de aplicación y casos de uso
- Separación clara entre comandos (`Language/Commands`) y consultas (`Language/Queries`)
- Handlers desacoplados para cada operación (`Language/CreateHandler`, `Language/GetByIdHandler`, etc.)
- Validadores específicos por tipo de request (`Language/CreateValidator`, etc.)
- Perfiles de mapeo organizados por entidad (`Language/CreateProfile`, etc.)

### 🔹 `Sakila.Contracts` – Modelos de comunicación (request/response)
- Contiene los `Request` y `Response` usados por los controladores y handlers
- Evita exponer detalles internos de la base de datos como nombres de columnas (`Id` en vez de `LanguageId`)
- Preparado para ser reutilizado por otros proyectos como `Sakila.Web`

### 🔹 `Sakila.Domain` – Entidades del dominio (mapeo del DBMS)
- Contiene los modelos `Entity Framework` (`Language`, `Film`, etc.)
- Propiedades como `LanguageId`, `FilmId`, etc., reflejan fielmente la estructura SQL Server

### 🔹 `Sakila.Infrastructure` – Infraestructura de acceso a datos
- Implementación del `DbContext` (`SakilaContext`)
- Conexión a SQL Server
- Configuración de EF Core

### 🔹 `Sakila.Web` – Razor Pages UI
- UI web desarrollada con Razor Pages (ASP.NET Core)
- Consumo de la API mediante `HttpClient`
- Separación de responsabilidades: sin lógica de negocio en la vista
- CRUD visual de entidades como películas, países, ciudades

---

## 🧰 Stack Tecnológico

| Capa         | Tecnología                    |
|--------------|-------------------------------|
| Backend      | ASP.NET Core 8.0 Web API      |
| ORM          | Entity Framework Core         |
| Frontend     | Razor Pages (ASP.NET Core)    |
| Base de Datos| SQL Server + Sakila           |
| IDE          | Visual Studio 2022 Community  |

---

## ✅ Criterios de evaluación cubiertos

| Criterio                              | Cumplimiento   |
|---------------------------------------|----------------|
| `DbContext` y ORM Framework           | ✅ EF Core      |
| `Entity object`                       | ✅ Modelos C#   |
| `Model (List<entity>)`               | ✅ Razor Pages  |
| Arquitectura (REST, MVC, Razor, etc) | ✅ Separado en API y Web |
| Video explicativo (opcional)         | 🚧 Por definir  |

---

## 👥 Equipo de trabajo

- **Víctor Martín Blanco Núñez**  
- **Anthony Manuel Burgos Reyes**

---

## 📄 Documentación de entrega

- El archivo `.docx` anexo incluye:
  - Presentación del equipo en formato APA
  - Resumen técnico de 250 palabras
  - Capturas, referencias y enlaces a GitHub

---

## 🚀 Cómo ejecutar el proyecto

1. Clona el repositorio
2. Ejecutar los scripts en 'Sakila SQL Server' en orden en SQL Server para la creacion de la base de datos Sakila
3. Configura la cadena de conexión a tu base de datos Sakila en `appsettings.json`
4. Ejecuta `Sakila.API` (API)
5. Ejecuta `Sakila.Web` (cliente Razor)
6. Navega a `/films`, `/cities`, etc. para interactuar con el sistema

---

## 📚 Referencias

- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)
- [SQL Server Documentation](https://learn.microsoft.com/en-us/sql/)
- [SQL Server Sakila Sample DB](https://github.com/jOOQ/sakila/tree/main)