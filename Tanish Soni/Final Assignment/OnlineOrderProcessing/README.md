# Online Order Processing & Inventory System

An ASP.NET Core MVC based Online Order Processing and Inventory Management System.
 
# Use this for login 
 
 # As Admin 
 
  - Email="admin1212@gmail.com"
  - Password="Admin@1212"
  
 # As Customer

  - Email="customer1212@gmail.com"
  - Password="Admin@1212"
     
# 1. Tools Used

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
- Code to database approach
---

# Main Features

## Customer Features

Customers can:

- Register 
- Login
- Logout
- Products Page
- Cart Page
- Add products to cart
- Update cart quantity
- Remove product from cart
- Create an order
- Make a Demo Payment
- Retry payment up to 3 times
- Orders Page

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

``` example
For example:

Delivered -> Processing

is not allowed.

Similarly:

Shipped -> Paid

is not allowed.
```

All order status transition rules are written in OrderWorkflowService.

---

# 5. Demo Payment

A random number generate using thing handle demo payment

- Success -  60%
- Failure -  30%
- Timeout -  10%

Every payment attempt is stored in the database.


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

To optimis concurrency use RowVersion and Include a byte[] RowVersion property in product entity and in Ef core configue it to IsRowVersion()


```csharp

public byte[] RowVersion { get; set; } = [];
builder.Entity<Product>()
.Property(p => p.RowVersion)
.IsRowVersion();

```
- 

```Example
Example:
Available Stock = 5
20 users attempt to purchase 1 item each
Maximum successful purchases <= 5
Final Stock >= 0
```
## Duplicate Request Detection

When user create a order than it also send a unique OrderRequest Key using this we find the order if it already made and return 
the same orderId

```csharp
var existingOrder =
await _unitOfWork.Order.GetOrderByKey(
Model.OrderRequestKey);
```


```database
modelBuilder.Entity<Order>()
.HasIndex(x => x.OrderRequestKey)
.IsUnique();
```
