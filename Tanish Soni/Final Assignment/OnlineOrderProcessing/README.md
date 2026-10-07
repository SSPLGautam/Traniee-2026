# Online Order Processing & Inventory System

An ASP.NET Core MVC based Online Order Processing and Inventory Management System.

Include all the functionlity that is include in the assigment 

---

# 1. Technologies Used

## Backend

- ASP.NET Core MVC
- .NET 10
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity

## Frontend

- Razor Views
- HTML
- CSS
- JavaScript
- jQuery
- Bootstrap

## Authentication

- ASP.NET Core Identity
- Cookie Authentication
- Role-Based Authorization

## Database

- Microsoft SQL Server
- Entity Framework Core Migrations

---

# 2. Main Features

## Customer Features

Customers can:

- Register an account
- Login
- Logout
- View products
- View product details
- Add products to cart
- Update cart quantity
- Remove products from cart
- Create an order
- Make a simulated payment
- Retry payment up to 3 times
- View their orders
- Track order status

## Admin Features

Administrators can:

- Login as Admin
- Add products
- Edit products
- Delete products
- View all products
- View all orders
- View failed/cancelled orders
- Update order status
- View order details
- View payment attempts
- View order events
- Generate sales reports

---

# 3. Order Processing Flow

The complete order processing flow is:

Customer
   |
Browse Products
   |
Add Product to Cart
   |
Create Order
   |
Check Duplicate OrderRequestKey
   |
Check Stock
   |
Reserve Stock
   |
Create Order
   |
Pending
   |
Payment
   |
   if (Success) than Paid 
   else (timeout) -> Retry
   else (Fail) -> Retry

---

# 4. Order Status Workflow

The application uses the following order status workflow:

Pending → Paid → Processing → Shipped → Delivered

Pending → Cancelled (payment failed 3 times, or cancelled by 

user/admin) → stock returned

Invalid status transitions are rejected.

For example:

Delivered -> Processing

is not allowed.

Similarly:

Shipped -> Paid

is not allowed.

All order status transition rules are handled by the OrderWorkflowService.

---

# 5. Payment Simulation

The application contains a simulated payment system.

Each payment attempt randomly produces one of the following results:

- Success - approximately 60%
- Failure - approximately 30%
- Timeout - approximately 10%

Every payment attempt is stored in the database.

Example:

Attempt 1 -> Failed
Attempt 2 -> Failed
Attempt 3 -> Success

The order then becomes:

Paid

If all three attempts fail or timeout:

Attempt 1 -> Failed
Attempt 2 -> Timeout
Attempt 3 -> Failed

Then:

Order -> Cancelled
Stock -> Released

A maximum of 3 payment attempts is allowed.

---

# 6. Database Entities

## Product

Product contains:

- Id
- SKU
- Name
- Price
- Stock
- RowVersion

Example:

```csharp
public class Product
{
    public Guid Id { get; set; }

    public string SKU { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public byte[] RowVersion { get; set; } = [];
}
```

## Project Structure

OnlineOrderProcessing/
|-Readme.md
|-ConcurrencyDemo
|_OnlineOrderProcessing/
   ├── Controllers/
   ├── Data/
   ├── Models/
   ├── Repositories/
   ├── Services/
   ├── ViewModels/
   ├── Views/
   ├── wwwroot/
   │   ├── css/
   │      └── js/
   ├── Migrations/
   ├── Program.cs
   └── appsettings.json

 

## Overselling Prevention

Overselling is prevented using optimistic concurrency with RowVersion. The Product entity contains a byte[]

RowVersion property and EF Core configures it using IsRowVersion().

```csharp

public byte[] RowVersion { get; set; } = [];
builder.Entity<Product>()
.Property(p => p.RowVersion)
.IsRowVersion();

```
- When two users try to purchase the same product at the same time, both may initially read the same stock and
RowVersion.

- The first request successfully updates the product and SQL Server changes the RowVersion. The second
request uses the old RowVersion, so EF Core detects the conflict and throws DbUpdateConcurrencyException. 
The
- failed transaction is rolled back. Therefore concurrent requests cannot silently overwrite stock changes and overselling
is prevented.

```Example
Example:
Available Stock = 5
20 users attempt to purchase 1 item each
Maximum successful purchases <= 5
Final Stock >= 0
```
## Duplicate Request Detection

Duplicate requests are detected using OrderRequestKey. Before creating an order, the application checks whether an
order with the same key already exists. If it exists, the existing order is returned instead of creating another order.

```csharp
var existingOrder =
await _unitOfWork.Order.GetOrderByKey(
Model.OrderRequestKey);
```

A unique database index provides an additional database-level guarantee:

```database
modelBuilder.Entity<Order>()
.HasIndex(x => x.OrderRequestKey)
.IsUnique();
```
Therefore one OrderRequestKey can create only one order. This protects against double-clicks, network retries,
repeated requests, and accidental duplicate submissions