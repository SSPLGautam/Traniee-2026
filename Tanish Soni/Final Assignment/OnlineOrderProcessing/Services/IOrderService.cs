using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IOrderService
    {
        Task<Result< CreateOrderResponseViewModel>> Create(CreateOrderViewModel Model);
        Task<Result< OrderListViewModel>> GetOrders();
        Task<Result<AdminOrdersViewModel>> GetAllOrders(
         int page = 1,
         int pageSize = 10);
        Task<Result<bool>> UpdateOrderStatus(
                Guid orderId,
          OrderStatus newStatus);
        Task<Result<AdminOrdersViewModel>> GetAllFailedOrders();


    }
}
