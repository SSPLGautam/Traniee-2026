using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.Repositories.Implementations;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.Services.Implementations;
using OnlineOrderProcessing.ViewModels;

const string connectionString =
    "Server=DESKTOP-RJCHHT1;Database=OnlineOrderProcessing;Trusted_Connection=True;TrustServerCertificate=True;";


var services = new ServiceCollection();

services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

services.AddHttpContextAccessor();

services.AddScoped<IUnitOfWork, UnitOfWork>();
services.AddScoped<IOrderService, OrderService>();
services.AddScoped<IOrderWorkflowService, OrderWorkflowService>();

var provider = services.BuildServiceProvider();


async Task<Result<CreateOrderResponseViewModel>> Attempt(
    Guid productId,
    string key)
{
    using var scope = provider.CreateScope();

    var service =
        scope.ServiceProvider.GetRequiredService<IOrderService>();

    var result = await service.Create(
        new CreateOrderViewModel
        {
            OrderRequestKey = key,

            Items = new List<CreateOrderItemViewModel>
            {
                new CreateOrderItemViewModel
                {
                    ProductId = productId,
                    Quantity = 1
                }
            }
        });

    return result;
}


var productId =
    Guid.Parse("8F4C7D2A-91E5-4B6A-A123-7C9D2E8F4510");


var gate = new TaskCompletionSource();

var tasks = Enumerable
    .Range(0, 20)
    .Select(_ => Task.Run(async () =>
    {
        await gate.Task;

        return await Attempt(
            productId,
            Guid.NewGuid().ToString());
    }))
    .ToList();


gate.SetResult();


var results = await Task.WhenAll(tasks);


Console.WriteLine("TEST 1:");

Console.WriteLine(
    $"Succeeded: {results.Count(r => r.IsSuccess)}");

Console.WriteLine(
    $"Failed:    {results.Count(r => r.IsFailure)}");

