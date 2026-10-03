using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IOrderService
    {
        Task<CreateOrderResponseViewModel> Create(CreateOrderViewModel Model);
        Task<OrderListViewModel> GetOrders();
    }
}
