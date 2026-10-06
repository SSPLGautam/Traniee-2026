using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.Repositories.Implementations;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.Services.Implementations;
using OnlineOrderProcessing.ViewModels;

var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        "Server=DESKTOP-RJCHHT1;Database=OnlineOrderProcessing;Trusted_Connection=True;TrustServerCertificate=True;"));

services.AddHttpContextAccessor();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IOrderWorkflowService, OrderWorkflowService>();

var provider = services.BuildServiceProvider();

var productId = Guid.Parse(
    "8F4C7D2A-91E5-4B6A-A124-7C9D2E8F4510");

var tasks = new List<Task<CreateOrderResponseViewModel>>();

for (int i = 0; i < 20; i++)
{
    var scope = provider.CreateScope();

    var orderService = scope.ServiceProvider
        .GetRequiredService<IOrderService>();

    var model = new CreateOrderViewModel
    {
        OrderRequestKey = Guid.NewGuid().ToString(),

        Items = new List<CreateOrderItemViewModel>
        {
            new CreateOrderItemViewModel
            {
                ProductId = productId,
                Quantity = 1
            }
        }
    };

    tasks.Add(CreateOrder(orderService, model, scope));
}

var results = await Task.WhenAll(tasks);

Console.WriteLine($"Success: {results.Count(x => x.Success)}");
Console.WriteLine($"Failed: {results.Count(x => !x.Success)}");


static async Task<CreateOrderResponseViewModel> CreateOrder(
    IOrderService orderService,
    CreateOrderViewModel model,
    IServiceScope scope)
{
    try
    {
        return await orderService.Create(model);
    }
    finally
    {
        scope.Dispose();
    }
}