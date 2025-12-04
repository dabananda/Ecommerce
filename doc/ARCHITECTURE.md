# 🏗️ Architecture & Design Patterns

## Repository Pattern
We utilize the **Repository Pattern** to decouple the business logic from Data Access.
- **Interfaces**: Located in `Repositories/Interfaces`. Define the contract.
- **Implementations**: Located in `Repositories/Implementations`. Contain EF Core logic.

## Service Layer
Controllers should remain thin. Business logic (validations, calculations, external API calls) resides in the **Service Layer**.
- **Services**: `Services/Implementations`.
- **Example**: `OrderService` handles stock checking, stock deduction, and cart clearing during order creation.

## Background Services
`StockReleaseService.cs` is a `BackgroundService` that runs periodically.
- **Goal**: Check for orders that are `Pending` (unpaid) and older than 30 minutes.
- **Action**: Cancels the order and releases the reserved stock back to the `Products` table.

## Database Design
We use **SQL Server** with **EF Core Code First**.

### Key Relationships
- **Category** (Self-Referencing): Supports sub-categories.
- **Product** -> **Category**: One-to-Many.
- **Order** -> **OrderItem**: One-to-Many (Owned Entity: Address).
- **User** -> **Cart**: One-to-One logic via BuyerId.

## Authentication Flow
1. **Registration**: User registers -> Email Token Generated -> Email Sent.
2. **Login**: User validates credentials -> Server returns **JWT (Short lived)** and **Refresh Token (Long lived)**.
3. **Protected Requests**: Client sends JWT in `Authorization: Bearer` header.
4. **Token Expiry**: Client hits `/auth/refresh-token` with the refresh token to get a new JWT.