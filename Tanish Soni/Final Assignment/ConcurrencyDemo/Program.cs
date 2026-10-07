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


var productId = Guid.Parse("8F4C7D2A-91E5-4B6A-A123-7C9D2E8F4510");

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

Console.WriteLine("TEST 1:");
Console.WriteLine($"Succeeded: {results.Count(r => r.Response.Success)} ");
Console.WriteLine($"Failed:    {results.Count(r => !r.Response.Success)} ");
foreach (var g in results.Where(r => !r.Response.Success).GroupBy(r => r.Response.Message))
    Console.WriteLine($"   {g.Count()} x \"{g.Key}\"");


record CreateOrderViewModelResult(CreateOrderResponseViewModel Response);