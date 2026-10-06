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


        public async Task<CreateOrderResponseViewModel> Create(CreateOrderViewModel Model)
        {
            //var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = "28b11383-bf4e-4132-8c7c-a851a3cf8a35";

            var existingOrder = await _unitOfWork.Order.GetOrderByKey(Model.OrderRequestKey);
            if (existingOrder != null)
            {
                return new CreateOrderResponseViewModel
                {
                    Success = true,
                    Message = "Order Exit",
                    OrderId = existingOrder.Id,
                    Status = existingOrder.Status
                };
            }
            await _unitOfWork.BeginTransactionAsync();

            try
            {

                var productExist = true;
                foreach (var item in Model.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        productExist = false;
                    }
                }
                if (!productExist)  
                {
                    return new CreateOrderResponseViewModel
                    {
                        Success = false,
                        Message = "One or more products do not exist."
                    };
                }
                foreach (var item in Model.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    if (product.Stock < item.Quantity)
                    {
                        return new CreateOrderResponseViewModel
                        {
                            Success = false,
                            Message = $"Not enough stock for {product.Name}"
                        };
                    }
                }
                decimal totalAmount = 0;
                foreach (var item in Model.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    totalAmount += product.Price * item.Quantity;
                }
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    Status = OrderStatus.Pending,
                    OrderRequestKey = Model.OrderRequestKey,
                    UserId = userId,
                    TotalAmount = totalAmount
                };

                await _unitOfWork.Order.AddAsync(order);

                foreach (var item in Model.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);

                    product.Stock -= item.Quantity;

                    var orderItem = new OrderItems
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.Price
                    };

                    await _unitOfWork.OrderItem.AddAsync(orderItem);
                }

                var orderEvent = new OrderEvent
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    OrderId = order.Id,
                    Details = "An Order is Created",
                    EventType = OrderEventType.OrderCreated
                };

                await _unitOfWork.OrderEvent.AddAsync(orderEvent);

                var cartItems = await _unitOfWork.CartItem.GetAllByUserId(userId);

                foreach (var item in cartItems)
                {
                    _unitOfWork.CartItem.Delete(item);
                }

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
            catch (DbUpdateConcurrencyException)
            {
                await _unitOfWork.RollbackTransactionAsync();

                return new CreateOrderResponseViewModel
                {
                    Success = false,
                    Message = "Stock was updated by another user. Please try again."
                };
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();

                return new CreateOrderResponseViewModel
                {
                    Success = false,
                    Message = "Unable to Create !"
                };

            }
        }


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