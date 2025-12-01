# ECommerce API

A robust, RESTful Web API for an E-Commerce platform built with **.NET 10**, **Entity Framework Core**, and **SQL Server**. This project demonstrates enterprise-grade patterns including Repository Design, Layered Architecture, JWT Authentication, and Structured Logging.

## 🚀 Features

* **CRUD Operations**: Full management of Products.
* **Advanced Data Access**: Pagination, Filtering, and Sorting.
* **Security**:
    * JWT (JSON Web Token) Authentication.
    * Secure Password Hashing (BCrypt).
    * Role-Based Authorization.
* **Validation**: Robust input validation using **FluentValidation**.
* **Architecture**:
    * Repository Pattern.
    * Service Layer (Business Logic isolation).
    * DTOs (Data Transfer Objects) with AutoMapper.
* **Resilience**: Global Exception Handling (RFC 7807 Problem Details).
* **Auditing**: Automated `CreatedAt` and `UpdatedAt` tracking via EF Core Interceptors.
* **Logging**: Structured logging using **Serilog** (Console + File sinks).

## 🛠 Tech Stack

* **Framework**: .NET 10 (ASP.NET Core Web API)
* **Database**: SQL Server
* **ORM**: Entity Framework Core
* **Libraries**:
    * `AutoMapper`: Object mapping.
    * `FluentValidation`: Input validation rules.
    * `BCrypt.Net`: Password security.
    * `Serilog`: Structured logging.
    * `System.IdentityModel.Tokens.Jwt`: Token generation.

## ⚙️ Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Docker)

### Installation

1.  **Clone the repository**
    ```bash
    git clone https://github.com/yourusername/ecommerce-api.git
    cd ecommerce-api
    ```

2.  **Configure Database**
    Update the `ConnectionStrings` in `appsettings.json` if needed. Default points to LocalDB:
    ```json
    "ConnectionStrings": {
      "ECommerceDbConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true"
    }
    ```

3.  **Apply Migrations**
    Run the following command to create the database and tables:
    ```bash
    dotnet ef database update
    ```

4.  **Run the Application**
    ```bash
    dotnet run --project ECommerce.Api
    ```

## 🔌 API Endpoints

### **Authentication**
| Method | Endpoint | Description | Auth Required |
| :--- | :--- | :--- | :--- |
| `POST` | `/api/auth/register` | Register a new user | ❌ |
| `POST` | `/api/auth/login` | Login and get JWT Token | ❌ |

### **Products**
| Method | Endpoint | Description | Auth Required |
| :--- | :--- | :--- | :--- |
| `GET` | `/api/products` | Get paged list of products | ❌ |
| `GET` | `/api/products/{id}` | Get specific product details | ❌ |
| `POST` | `/api/products` | Create a new product | ✅ (Bearer Token) |
| `PUT` | `/api/products/{id}` | Update an existing product | ✅ (Bearer Token) |
| `DELETE` | `/api/products/{id}` | Delete a product | ✅ (Bearer Token) |

## 🧪 Testing with Postman

1.  **Register**: Send a POST to `/api/auth/register` with `username` and `password`.
2.  **Login**: Send a POST to `/api/auth/login` to receive a `token`.
3.  **Access Protected Routes**:
    * Copy the `token`.
    * In Postman, go to the **Headers** tab of your request (e.g., Create Product).
    * Key: `Authorization`
    * Value: `Bearer <YOUR_TOKEN>`

## 📝 Logging
Logs are written to the console and daily rolling files in the `Logs/` directory.
