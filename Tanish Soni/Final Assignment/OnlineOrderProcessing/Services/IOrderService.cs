using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IOrderService
    {
        Task<CreateOrderResponseViewModel> Create(CreateOrderViewModel Model);
        Task<OrderListViewModel> GetOrders();
        Task<AdminOrdersViewModel> GetAllOrders();
        Task<Result> UpdateOrderStatus(
                Guid orderId,
          OrderStatus newStatus);
        Task<AdminOrdersViewModel> GetAllFailedOrders();
     
    }
}
