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
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<CreateOrderResponseViewModel> Create(CreateOrderViewModel Model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
           
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
                foreach(var item in Model.Items)
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
                foreach(var item in Model.Items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    if(product.Stock < item.Quantity)
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
                    Status = OrderStatus.Processing,
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
                        OrderId= order.Id,
                        ProductId=product.Id,
                        Quantity=item.Quantity,
                        UnitPrice= item.Quantity*product.Price
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
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();

                return new CreateOrderResponseViewModel { 
                    Success=false,
                    Message="Unable to Create !"
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

            return new OrderListViewModel { Orders =ordersVM };


        }
    }
}
