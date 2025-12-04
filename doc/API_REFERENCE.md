# API Endpoint Reference

Base URL: `https://localhost:7029/api`

## 🔐 Authentication (`/auth`)

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `POST` | `/register` | Register a new user account. Triggers email verification. | No |
| `POST` | `/login` | Authenticate user. Returns `token` and `refreshToken`. | No |
| `POST` | `/refresh-token` | Obtain a new access token using a valid refresh token. | No |
| `GET` | `/verify-email` | Confirm email address using token from email link. | No |
| `POST` | `/forgot-password` | Initiate password reset flow. | No |
| `POST` | `/reset-password` | Complete password reset with token. | No |

## 📦 Products (`/products`)

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Get paged products. Query Params: `PageNumber`, `PageSize`, `SearchTerm`, `OrderBy` (priceAsc/Desc). | No |
| `GET` | `/{id}` | Get detailed product info including images. | No |
| `POST` | `/` | Create Product. Form-Data required (includes image file). | **Admin** |
| `PUT` | `/{id}` | Update Product details. | **Admin** |
| `DELETE` | `/{id}` | Soft delete or hard delete a product. | **Admin** |

## 🛒 Shopping Cart (`/cart`)

*Note: Carts are linked to a `buyerId` (Username if logged in, or generated GUID for guests).*

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `GET` | `/` | Retrieve current cart. | Optional |
| `POST` | `/` | Add item to cart. Query: `productId`, `quantity`. | Optional |
| `DELETE` | `/` | Remove specific item. | Optional |
| `DELETE` | `/clear` | Delete entire cart. | Optional |

## 🧾 Orders (`/orders`)

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `POST` | `/` | Convert Cart to Order. Validates stock. | **Yes** |
| `GET` | `/` | Get logged-in user's order history. | **Yes** |
| `GET` | `/{id}` | Get specific order details. | **Yes** |
| `GET` | `/admin` | Get all orders (Paged). Filter by `Status`. | **Admin** |
| `PATCH` | `/{id}/status` | Update Order Status (Pending/Shipped/etc). | **Admin** |

## 💳 Payments (`/payments`)

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `POST` | `/{orderId}` | Create/Update Stripe Payment Intent. Returns `clientSecret`. | **Yes** |
| `POST` | `/webhook` | Stripe Webhook handler. Updates order status automatically. | No |

## ⭐ Reviews & Wishlist

| Method | Endpoint | Description | Auth |
| :--- | :--- | :--- | :--- |
| `GET` | `/products/{id}/reviews` | Get reviews for a product. | No |
| `POST` | `/products/{id}/reviews` | Add review. *User must have purchased item.* | **Yes** |
| `GET` | `/wishlist` | Get user's wishlist. | **Yes** |
| `POST` | `/wishlist/{productId}` | Toggle (Add/Remove) item in wishlist. | **Yes** |