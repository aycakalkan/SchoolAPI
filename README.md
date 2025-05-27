# SchoolAPI

A lightweight *ASP-NET Core 7* REST service that models a small school:

* *Ogrenciler* – students  
* *Ogretmenler* – teachers  
* *Dersler* – courses  
* *OgrenciDersler* – join-table for enrolments  

Built with clean layers (Controllers → Services → EF Core), automatic database migrations, full Swagger docs, and an automated test suite.

---

## Table of Contents
1. [Architecture](#architecture)  
2. [Project Structure](#project-structure)  
3. [Getting Started](#getting-started)  
4. [Database Schema](#database-schema)  
5. [REST Endpoints & Examples](#rest-endpoints--examples)  
   5.1 [Ogretmenler](#1-ogretmenler-teachers) · 5.2 [Dersler](#2-dersler-courses) · 5.3 [Ogrenciler](#3-ogrenciler-students) · 5.4 [OgrenciDersler](#4-ogrencidersler-join-table)  
6. [Running the Tests](#running-the-tests)  
7. [Contributing](#contributing)  
8. [License](#license)

---

## Architecture
Controllers (Web API) │ ← Presentation
Services (Domain) │ ← Business logic
EF Core DbContext │ ← Data / Infrastructure

* *Controllers* – thin; validate input & call services  
* *Services* – hold business rules and orchestrate EF Core  
* *Migrations* – keep SQL schema in sync with C# models  
* *Swagger UI* – interactive docs at **/swagger**

---

## Project Structure

| Path / File            | Purpose |
|------------------------|---------|
| Controllers/         | HTTP entry points (*Controller.cs) |
| Services/            | Domain logic (*Service.cs) |
| Models/              | POCO entities with *Data Annotations* |
| Migrations/          | EF Core migrations & snapshot |
| Program.cs           | Minimal-API bootstrap (DI, Swagger, CORS) |
| SchoolApi.http       | Ready-made REST calls for VS Code + REST Client |

---

## Getting Started

```bash
# 1 Clone
git clone https://github.com/aycakalkan/SchoolAPI.git
cd SchoolAPI

# 2 Restore & build
dotnet restore
dotnet build

# 3 Apply migrations & run
dotnet ef database update     # uses SqlLocalDB by default
dotnet run

# 4 Open Swagger docs
start https://localhost:5001/swagger   # use open on macOS/Linux


| Table              | Columns (non-nullable *bold*)                                                       |
| ------------------ | ------------------------------------------------------------------------------------- |
| *Ogrenciler*     | *Id* (PK identity), *Ad*                                                        |
| *Ogretmenler*    | *Id, **Ad*                                                                        |
| *Dersler*        | *Id, **DersAdi, **OgretmenId* (FK → Ogretmenler.Id)                           |
| *OgrenciDersler* | *OgrenciId* (FK → Ogrenciler.Id), *DersId* (FK → Dersler.Id) — composite PK |

