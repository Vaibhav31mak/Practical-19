# Practical19

## Project Overview

**Practical19** is an ASP.NET Core MVC (.NET 10) application that demonstrates:

- ASP.NET Core Identity authentication
- Role-based and policy-based authorization
- User and role management
- Secure login/logout functionality
- Admin-restricted operations
- Clean layered architecture using services and interfaces

The project is built using modern ASP.NET Core MVC practices with Entity Framework Core and SQL Server.

---

# Features

- ASP.NET Core Identity authentication
- User registration and login
- Role management
- Policy-based authorization
- Admin-only protected pages
- User management
- Razor Views UI
- Entity Framework Core with SQL Server
- Dependency Injection
- Clean separation using Services and Interfaces

---

# Tech Stack

| Technology | Purpose |
|---|---|
| ASP.NET Core MVC (.NET 10) | Web Framework |
| Entity Framework Core | ORM |
| SQL Server LocalDB | Database |
| ASP.NET Core Identity | Authentication & Authorization |
| Razor Views | Frontend UI |
| Bootstrap | Styling |
| Dependency Injection | Loose Coupling |

---

# Prerequisites

Before running the project, install:

- [.NET 10 SDK](https://dotnet.microsoft.com/)
- SQL Server or SQL Server LocalDB
- Visual Studio 2022+ / VS Code
- Entity Framework Core Tools

Install EF Core CLI tools:

```bash
dotnet tool install --global dotnet-ef
---

# Setup Instructions

## 1. Clone Repository

```bash
git clone -b feature <repository-url>
cd Practical19
```

---

## 2. Configure Database Connection

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=Practical19Db;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

---

# Database Setup & Migrations

## Apply Existing Migrations

```bash
dotnet ef database update
```

## Create New Migration

```bash
dotnet ef migrations add InitialCreate
```

## Update Database

```bash
dotnet ef database update
```

---

# Run the Application

## Using CLI

```bash
dotnet run
```

## Using Visual Studio

* Open `Practical19.sln`
* Set `Practical19` as Startup Project
* Press `F5`

Application URLs are available in:

```text
Properties/launchSettings.json
```

---

# Default Admin Credentials

A default admin user is seeded automatically.

| Role  | Email                                     | Password  |
| ----- | ----------------------------------------- | --------- |
| Admin | [admin@gmail.com](mailto:admin@gmail.com) | Admin@123 |

---

# Usage Notes

## Admin Permissions

Admin users can:

* Manage roles
* Manage users
* Access protected admin pages
* Assign roles to users
* Perform restricted operations

## Normal User Permissions

Normal users can:

* Register/Login
* Access authorized user pages
* View permitted resources

Users without required policies/roles cannot access protected endpoints.

---

# Folder Structure

```text
Practical19/
│
├── Controllers/
│
├── Data/
│
├── Migrations/
│
├── Models/
│   └── ViewModels/
│
├── Services/
│   └── Interfaces/
│
├── Views/
│   ├── Account/
│   ├── Roles/
│   ├── Users/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
│
├── Properties/
│   └── launchSettings.json
│
├── appsettings.json
├── Program.cs
├── Practical19.csproj
```

---

# Authentication & Authorization

## Authentication

Implemented using:

* ASP.NET Core Identity
* Cookie Authentication
* Secure Password Hashing

## Authorization

Implemented using:

* Role-based authorization
* Policy-based authorization
* `[Authorize]` attributes
* Custom access policies

Example:

```csharp
[Authorize(Roles = "Admin")]
```

```csharp
[Authorize(Policy = "AdminPolicy")]
```

---

# Architecture Highlights

* MVC Architecture
* Service Layer Pattern
* Interface-based Abstractions
* Dependency Injection
* Separation of Concerns
* Scalable and Maintainable Structure

---

# Future Improvements

* JWT Authentication
* Refresh Tokens
* Email Verification
* Forgot Password
* Audit Logging
* Repository Pattern
* Unit Testing
* Docker Support

---
