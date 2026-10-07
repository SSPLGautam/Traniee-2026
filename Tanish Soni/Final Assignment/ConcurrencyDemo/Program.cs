using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.Repositories.Implementations;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.Services.Implementations;
using OnlineOrderProcessing.ViewModels;

const string connectionString =
    "Server=DESKTOP-RJCHHT1;Database=OnlineOrderProcessing;Trusted_Connection=True;TrustServerCertificate=True;";
const string userId = "28b11383-bf4e-4132-8c7c-a851a3cf8a35"; 

var services = new ServiceCollection();
services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(connectionString));
services.AddHttpContextAccessor();
services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IOrderWorkflowService, OrderWorkflowService>();
var provider = services.BuildServiceProvider();
async Task<Guid> SeedProduct(int stock)
{
    using var scope = provider.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var p = new Product
    {
        Id = Guid.NewGuid(),
        SKU = $"T-{Guid.NewGuid():N}"[..12],
        Name = "Concurrency Test",
        Price = 10m,
        Stock = stock
    };
    db.Products.Add(p);
    await db.SaveChangesAsync();
    return p.Id;
}
async Task<CreateOrderViewModelResult> Attempt(Guid productId, string key)
{
    using var scope = provider.CreateScope();
    var svc = scope.ServiceProvider.GetRequiredService<IOrderService>();
    var r = await svc.Create(new CreateOrderViewModel
    {
        OrderRequestKey = key,
        Items = new List<CreateOrderItemViewModel>
        {
            new() { ProductId = productId, Quantity = 1 }
        }
    });
    return new CreateOrderViewModelResult(r);
}

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!await db.Users.AnyAsync(u => u.Id == userId))
    {
        Console.WriteLine($"User {userId} does not exist in AspNetUsers. Register one and update the id.");
        return;
    }
}

var productId = await SeedProduct(stock: 5);

var gate = new TaskCompletionSource();  
var tasks = Enumerable.Range(0, 20)
    .Select(_ => Task.Run(async () =>
    {
        await gate.Task;
        return await Attempt(productId, Guid.NewGuid().ToString());
    }))
    .ToList();

gate.SetResult();
var results = await Task.WhenAll(tasks);

Console.WriteLine("=== TEST 1: stock race ===");
Console.WriteLine($"Succeeded: {results.Count(r => r.Response.Success)}   (expected 5)");
Console.WriteLine($"Failed:    {results.Count(r => !r.Response.Success)}   (expected 15)");
foreach (var g in results.Where(r => !r.Response.Success).GroupBy(r => r.Response.Message))
    Console.WriteLine($"   {g.Count()} x \"{g.Key}\"");

using (var scope = provider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var stock = await db.Products.AsNoTracking().Where(p => p.Id == productId).Select(p => p.Stock).SingleAsync();
    var orderItems = await db.OrderItems.CountAsync(i => i.ProductId == productId);
    Console.WriteLine($"Final stock: {stock}   (expected 0)");
    Console.WriteLine($"Order items for product: {orderItems}   (expected 5)");
}


record CreateOrderViewModelResult(CreateOrderResponseViewModel Response);