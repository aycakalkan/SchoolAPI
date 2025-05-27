# SchoolAPI

A lightweight ASP.NET Core 7 REST API service that models a small school management system with full CRUD operations for students, teachers, courses, and student enrollments.

## Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
  - [Ogretmenler (Teachers)](#ogretmenler-teachers)
  - [Dersler (Courses)](#dersler-courses)
  - [Ogrenciler (Students)](#ogrenciler-students)
  - [OgrenciDersler (Student Enrollments)](#ogrencidersler-student-enrollments)
- [Running Tests](#running-tests)
- [Technologies Used](#technologies-used)
- [Contributing](#contributing)

## Overview

SchoolAPI is a RESTful web service designed to manage basic school operations, including:

- **Ogrenciler** – Students management
- **Ogretmenler** – Teachers management  
- **Dersler** – Courses management
- **OgrenciDersler** – Student-course enrollment relationships

The API is built with clean architecture principles, featuring automatic database migrations, comprehensive Swagger documentation, and a complete automated test suite.

## Architecture

```
Controllers (Web API) 
    ↓
Services (Domain Logic)
    ↓
EF Core DbContext (Data Access)
```

**Key Architectural Principles:**
- **Controllers**: Thin layer handling HTTP requests and responses
- **Services**: Business logic and domain rules implementation
- **EF Core**: Data access layer with Entity Framework Core
- **Clean Separation**: Clear boundaries between presentation, business, and data layers

## Project Structure

```
SchoolAPI/
├── Controllers/           # HTTP API endpoints
│   ├── OgrencilerController.cs
│   ├── OgretmenlerController.cs
│   ├── DerslerController.cs
│   └── OgrenciDerslerController.cs
├── Services/             # Business logic layer
│   ├── OgrenciService.cs
│   ├── OgretmenService.cs
│   ├── DersService.cs
│   └── OgrenciDersService.cs
├── Models/               # Entity models with Data Annotations
│   ├── Ogrenci.cs
│   ├── Ogretmen.cs
│   ├── Ders.cs
│   └── OgrenciDers.cs
├── Data/                 # Database context
│   └── SchoolContext.cs
├── Migrations/           # EF Core migrations
├── Tests/                # Unit and integration tests
├── Program.cs            # Application bootstrap
└── SchoolApi.http        # REST Client test requests
```

## Getting Started

### Prerequisites
- .NET 7.0 SDK or later
- SQL Server LocalDB (or SQL Server)
- Visual Studio 2022 or VS Code

### Installation & Setup

1. **Clone the repository**
```bash
git clone https://github.com/aycakalkan/SchoolAPI.git
cd SchoolAPI
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Build the project**
```bash
dotnet build
```

4. **Apply database migrations**
```bash
dotnet ef database update
```

5. **Run the application**
```bash
dotnet run
```

6. **Access Swagger Documentation**
```
https://localhost:5001/swagger
```

## Database Schema

| Table | Columns | Description |
|-------|---------|-------------|
| **Ogrenciler** | Id (PK), Ad | Students table |
| **Ogretmenler** | Id (PK), Ad | Teachers table |
| **Dersler** | Id (PK), DersAdi, OgretmenId (FK) | Courses table |
| **OgrenciDersler** | OgrenciId (FK), DersId (FK) | Student-Course enrollments (junction table) |

### Relationships
- **Ogretmenler** → **Dersler** (One-to-Many): A teacher can teach multiple courses
- **Ogrenciler** ↔ **Dersler** (Many-to-Many): Students can enroll in multiple courses via OgrenciDersler

## API Endpoints

Base URL: `https://localhost:5001/api`

### Ogretmenler (Teachers)

#### GET /api/ogretmenler
**Description**: Retrieve all teachers

**Request Example:**
```http
GET https://localhost:5001/api/ogretmenler
Accept: application/json
```

**Response Example:**
```json
[
  {
    "id": 1,
    "ad": "Ahmet Yılmaz"
  },
  {
    "id": 2,
    "ad": "Fatma Demir"
  }
]
```

#### GET /api/ogretmenler/{id}
**Description**: Retrieve a specific teacher by ID

**Request Example:**
```http
GET https://localhost:5001/api/ogretmenler/1
Accept: application/json
```

**Response Example:**
```json
{
  "id": 1,
  "ad": "Ahmet Yılmaz"
}
```

#### POST /api/ogretmenler
**Description**: Create a new teacher

**Request Example:**
```http
POST https://localhost:5001/api/ogretmenler
Content-Type: application/json

{
  "ad": "Mehmet Kaya"
}
```

**Response Example:**
```json
{
  "id": 3,
  "ad": "Mehmet Kaya"
}
```

#### PUT /api/ogretmenler/{id}
**Description**: Update an existing teacher

**Request Example:**
```http
PUT https://localhost:5001/api/ogretmenler/1
Content-Type: application/json

{
  "id": 1,
  "ad": "Ahmet Yılmaz Güncellendi"
}
```

**Response Example:**
```json
{
  "id": 1,
  "ad": "Ahmet Yılmaz Güncellendi"
}
```

#### DELETE /api/ogretmenler/{id}
**Description**: Delete a teacher

**Request Example:**
```http
DELETE https://localhost:5001/api/ogretmenler/1
```

**Response**: `204 No Content`

---

### Dersler (Courses)

#### GET /api/dersler
**Description**: Retrieve all courses

**Request Example:**
```http
GET https://localhost:5001/api/dersler
Accept: application/json
```

**Response Example:**
```json
[
  {
    "id": 1,
    "dersAdi": "Matematik",
    "ogretmenId": 1
  },
  {
    "id": 2,
    "dersAdi": "Fizik",
    "ogretmenId": 2
  }
]
```

#### GET /api/dersler/{id}
**Description**: Retrieve a specific course by ID

**Request Example:**
```http
GET https://localhost:5001/api/dersler/1
Accept: application/json
```

**Response Example:**
```json
{
  "id": 1,
  "dersAdi": "Matematik",
  "ogretmenId": 1
}
```

#### POST /api/dersler
**Description**: Create a new course

**Request Example:**
```http
POST https://localhost:5001/api/dersler
Content-Type: application/json

{
  "dersAdi": "Kimya",
  "ogretmenId": 1
}
```

**Response Example:**
```json
{
  "id": 3,
  "dersAdi": "Kimya",
  "ogretmenId": 1
}
```

#### PUT /api/dersler/{id}
**Description**: Update an existing course

**Request Example:**
```http
PUT https://localhost:5001/api/dersler/1
Content-Type: application/json

{
  "id": 1,
  "dersAdi": "İleri Matematik",
  "ogretmenId": 2
}
```

**Response Example:**
```json
{
  "id": 1,
  "dersAdi": "İleri Matematik",
  "ogretmenId": 2
}
```

#### DELETE /api/dersler/{id}
**Description**: Delete a course

**Request Example:**
```http
DELETE https://localhost:5001/api/dersler/1
```

**Response**: `204 No Content`

---

### Ogrenciler (Students)

#### GET /api/ogrenciler
**Description**: Retrieve all students

**Request Example:**
```http
GET https://localhost:5001/api/ogrenciler
Accept: application/json
```

**Response Example:**
```json
[
  {
    "id": 1,
    "ad": "Ali Veli"
  },
  {
    "id": 2,
    "ad": "Ayşe Yılmaz"
  }
]
```

#### GET /api/ogrenciler/{id}
**Description**: Retrieve a specific student by ID

**Request Example:**
```http
GET https://localhost:5001/api/ogrenciler/1
Accept: application/json
```

**Response Example:**
```json
{
  "id": 1,
  "ad": "Ali Veli"
}
```

#### POST /api/ogrenciler
**Description**: Create a new student

**Request Example:**
```http
POST https://localhost:5001/api/ogrenciler
Content-Type: application/json

{
  "ad": "Zeynep Kara"
}
```

**Response Example:**
```json
{
  "id": 3,
  "ad": "Zeynep Kara"
}
```

#### PUT /api/ogrenciler/{id}
**Description**: Update an existing student

**Request Example:**
```http
PUT https://localhost:5001/api/ogrenciler/1
Content-Type: application/json

{
  "id": 1,
  "ad": "Ali Veli Güncellendi"
}
```

**Response Example:**
```json
{
  "id": 1,
  "ad": "Ali Veli Güncellendi"
}
```

#### DELETE /api/ogrenciler/{id}
**Description**: Delete a student

**Request Example:**
```http
DELETE https://localhost:5001/api/ogrenciler/1
```

**Response**: `204 No Content`

---

### OgrenciDersler (Student Enrollments)

#### GET /api/ogrencidersler
**Description**: Retrieve all student-course enrollments

**Request Example:**
```http
GET https://localhost:5001/api/ogrencidersler
Accept: application/json
```

**Response Example:**
```json
[
  {
    "ogrenciId": 1,
    "dersId": 1
  },
  {
    "ogrenciId": 1,
    "dersId": 2
  },
  {
    "ogrenciId": 2,
    "dersId": 1
  }
]
```

#### GET /api/ogrencidersler/{ogrenciId}/{dersId}
**Description**: Retrieve a specific enrollment

**Request Example:**
```http
GET https://localhost:5001/api/ogrencidersler/1/1
Accept: application/json
```

**Response Example:**
```json
{
  "ogrenciId": 1,
  "dersId": 1
}
```

#### POST /api/ogrencidersler
**Description**: Enroll a student in a course

**Request Example:**
```http
POST https://localhost:5001/api/ogrencidersler
Content-Type: application/json

{
  "ogrenciId": 2,
  "dersId": 2
}
```

**Response Example:**
```json
{
  "ogrenciId": 2,
  "dersId": 2
}
```

#### PUT /api/ogrencidersler/{ogrenciId}/{dersId}
**Description**: Update a student enrollment

**Request Example:**
```http
PUT https://localhost:5001/api/ogrencidersler/1/1
Content-Type: application/json

{
  "ogrenciId": 1,
  "dersId": 3
}
```

**Response Example:**
```json
{
  "ogrenciId": 1,
  "dersId": 3
}
```

#### DELETE /api/ogrencidersler/{ogrenciId}/{dersId}
**Description**: Remove a student from a course

**Request Example:**
```http
DELETE https://localhost:5001/api/ogrencidersler/1/1
```

## Technologies Used

- **ASP.NET Core 7**: Web API framework
- **Entity Framework Core**: Object-relational mapping
- **SQL Server LocalDB**: Database engine
- **Swagger/OpenAPI**: API documentation
- **xUnit**: Testing framework
- **AutoMapper**: Object mapping
- **FluentValidation**: Input validation
