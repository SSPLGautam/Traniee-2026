using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;
using System.Security.Claims;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderWorkflowService _orderWorkflowService;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IOrderWorkflowService orderWorkflowService)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _orderWorkflowService = orderWorkflowService;
        }


        public async Task<CreateOrderResponseViewModel> Create(CreateOrderViewModel model)
        {
            var userId = "28b11383-bf4e-4132-8c7c-a851a3cf8a35"; // TODO: real user

            if (model.Items == null || !model.Items.Any() || model.Items.Any(i => i.Quantity <= 0))
                return Fail("Invalid order items.");

            var items = model.Items
                .GroupBy(i => i.ProductId)
                .Select(g => new { ProductId = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .OrderBy(i => i.ProductId)
                .ToList();

            var existing = await _unitOfWork.Order.GetOrderByKey(model.OrderRequestKey);
            if (existing != null) return Existing(existing);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderRequestKey = model.OrderRequestKey,
                    UserId = userId
                };

                decimal total = 0;
                var orderItems = new List<OrderItems>();

                foreach (var item in items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Fail("One or more products do not exist.");
                    }

                    if (!await _unitOfWork.Products.TryDecreaseStockAsync(item.ProductId, item.Quantity))
                    {
                        await _unitOfWork.RollbackTransactionAsync();
                        return Fail($"Insufficient stock for {product.Name}");
                    }

                    total += product.Price * item.Quantity;
                    orderItems.Add(new OrderItems
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    });
                }

                order.TotalAmount = total;
                await _unitOfWork.Order.AddAsync(order);
                foreach (var oi in orderItems) await _unitOfWork.OrderItem.AddAsync(oi);

                await _unitOfWork.OrderEvent.AddAsync(new OrderEvent
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    OrderId = order.Id,
                    Details = "An Order is Created",
                    EventType = OrderEventType.OrderCreated
                });

                foreach (var cartItem in await _unitOfWork.CartItem.GetAllByUserId(userId))
                    _unitOfWork.CartItem.Delete(cartItem);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new CreateOrderResponseViewModel
                {
                    Success = true,
                    Message = "Order Created Successfully !!",
                    OrderId = order.Id,
                    Status = order.Status
                };
            }
            catch (DbUpdateException)  
            {
                await _unitOfWork.RollbackTransactionAsync();

                var winner = await _unitOfWork.Order.GetOrderByKey(model.OrderRequestKey);
                return winner != null ? Existing(winner) : Fail("Unable to Create !");
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Fail("Unable to Create !");
            }
        }

        private static CreateOrderResponseViewModel Fail(string message) =>
            new() { Success = false, Message = message };

        private static CreateOrderResponseViewModel Existing(Order o) =>
            new() { Success = true, Message = "Order already exists", OrderId = o.Id, Status = o.Status };

        public async Task<OrderListViewModel> GetOrders()
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _unitOfWork.Order.GetOrderByUserId(userId);

            var ordersVM = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                Items = o.OrderItems.ToList(),
                TotalAmount = o.TotalAmount,
                TotalItems = o.OrderItems.Count()
            }).ToList();

            return new OrderListViewModel { Orders = ordersVM };


        }

        public async Task<AdminOrdersViewModel> GetAllOrders()
        {

            var adminOrders = await _unitOfWork.Order.GetAllOrders();


            var orders = adminOrders.Select(o => new AdminOrderViewModel
            {

                OrderId = o.Id,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                PaymentAttempts = o.Payments
                                  .OrderByDescending(p => p.Attempt)
                                  .Select(p => p.Attempt)
            .FirstOrDefault(),
                CustomerEmail = o.User.Email,
                CustomerName = o.User.UserName,
                Items = o.OrderItems.ToList()



            });

            return new AdminOrdersViewModel
            {
                Orders = orders.ToList(),

                OrderStatuses = Enum
              .GetValues<OnlineOrderProcessing.Enums.OrderStatus>()
              .ToList()
            };
        }

        public async Task<AdminOrdersViewModel> GetAllFailedOrders()
        {

            var adminOrders = await _unitOfWork.Order.GetAllFailedOrders();


            var orders = adminOrders.Select(o => new AdminOrderViewModel
            {

                OrderId = o.Id,
                Status = o.Status,
                CreatedAt = o.CreatedAt,
                TotalAmount = o.TotalAmount,
                PaymentAttempts = o.Payments
                                  .OrderByDescending(p => p.Attempt)
                                  .Select(p => p.Attempt)
            .FirstOrDefault(),
                CustomerEmail = o.User.Email,
                CustomerName = o.User.UserName,
                Items = o.OrderItems.ToList()



            });

            return new AdminOrdersViewModel
            {
                Orders = orders.ToList(),

                OrderStatuses = Enum
              .GetValues<OnlineOrderProcessing.Enums.OrderStatus>()
              .ToList()
            };
        }



        public async Task<Result> UpdateOrderStatus(
    Guid orderId,
    OrderStatus newStatus)
        {
            var order = await _unitOfWork.Order.GetOrderById(orderId);

            if (order == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "Order not found"
                };
            }
            if (!_orderWorkflowService.CanChange(order.Status, newStatus))
            {
                return new Result
                {
                    Success = false,
                    Message = $"Cannot change {order.Status} to {newStatus}"
                };
            }

            using var transaction = await _unitOfWork.BeginTransactionAsync();

            try
            {
                var oldStatus = order.Status;
                if (newStatus == OrderStatus.Cancelled)
                {
                    foreach (var item in order.OrderItems)
                    {
                        item.Product.Stock += item.Quantity;
                    }

                    order.Status = newStatus;

                    _unitOfWork.Order.Update(order);

                    await _unitOfWork.OrderEvent.AddAsync(new OrderEvent
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        EventType = OrderEventType.OrderCancelled,
                        Details = $"Order changed from {oldStatus} to Cancelled.",
                        CreatedAt = DateTime.UtcNow
                    });

                    await _unitOfWork.OrderEvent.AddAsync(new OrderEvent
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        EventType = OrderEventType.StockReleased,
                        Details = "Reserved stock returned to inventory.",
                        CreatedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    order.Status = newStatus;

                    _unitOfWork.Order.Update(order);

                    await _unitOfWork.OrderEvent.AddAsync(new OrderEvent
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        EventType = GetStatusEvent(newStatus),
                        Details = $"Order status changed from {oldStatus} to {newStatus}.",
                        CreatedAt = DateTime.UtcNow
                    });
                }

                await _unitOfWork.SaveChangesAsync();

                await transaction.CommitAsync();

                return new Result
                {
                    Success = true,
                    Message = $"Order status changed to {newStatus}."
                };
            }
            catch
            {
                await transaction.RollbackAsync();

                return new Result
                {
                    Success = false,
                    Message = "Failed to update order status."
                };
            }
        }




        private OrderEventType GetStatusEvent(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Paid => OrderEventType.PaymentSucceeded,

                OrderStatus.Processing => OrderEventType.OrderConfirmed,

                OrderStatus.Shipped => OrderEventType.OrderShipped,

                OrderStatus.Delivered => OrderEventType.OrderDelivered,

                _ => OrderEventType.OrderCreated
            };
        }

         }
}