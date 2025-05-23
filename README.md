
# 🧠 Caso Práctico 2 – CRUD/ORM con MySQL y Sakila

**Maestría en Ciencia de Datos e Inteligencia Artificial (MACDIA)**  
**Materia:** Ciencia de Datos I – INF-7303-C1  
**Profesor:** Silverio Del Orbe A.  

---

## 🎯 Objetivo del proyecto

Este repositorio forma parte del segundo caso práctico colaborativo de la materia, en el cual se desarrolla un sistema CRUD/ORM sobre la base de datos Sakila (MySQL), aplicando los principios de programación orientada a objetos, estructuras de datos y arquitectura escalable para el consumo estandarizado de datos.

---

## 🧱 Arquitectura de la solución

El sistema está compuesto por dos proyectos separados que siguen buenas prácticas de desacoplamiento y responsabilidad única:

### 🔹 `Sakila.API` – ASP.NET Core Web API
- **Entity Framework Core** (ORM)
- `DbContext` (`SakilaContext`) para mapear la base de datos
- `Entities` como mapeo de las tablas del DBMS
- Controladores RESTful (`FilmController`, `CityController`, etc.)
- Exposición de endpoints API (`/api/films`, `/api/countries`, ...)

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
| Base de Datos| MySQL + Sakila                |
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
2. Configura la cadena de conexión a tu base de datos Sakila en `appsettings.json`
3. Ejecuta `Sakila.API` (API)
4. Ejecuta `Sakila.Web` (cliente Razor)
5. Navega a `/films`, `/cities`, etc. para interactuar con el sistema

---

## 📚 Referencias

- [Entity Framework Core Docs](https://learn.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/)
- [MySQL Sakila Sample DB](https://dev.mysql.com/doc/sakila/en/)
