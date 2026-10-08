using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByKey(string OrderRequestKey);
        Task<List<Order>> GetOrderByUserId(string UserId);
        Task<Order> GetOrderById(Guid OrderId);
        Task<List<Order>> GetAllOrders(int page, int pageSize);
        Task<List<Order>> GetAllFailedOrders();
        Task<int> GetOrdersCount();


    }
}
