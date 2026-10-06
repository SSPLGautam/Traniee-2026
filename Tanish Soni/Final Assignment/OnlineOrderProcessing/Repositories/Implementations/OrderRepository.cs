using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class OrderRepository:GenericRepository<Order> , IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
       public async  Task<Order?> GetOrderByKey(string OrderRequestKey)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderRequestKey == OrderRequestKey);
        }
        public async Task<List<Order>> GetOrderByUserId(string UserId)
        {
            return await _context.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Product)
                .OrderByDescending(o=>o.CreatedAt)
                .Where(o => o.UserId == UserId).ToListAsync();
        }

        public async Task<Order> GetOrderById(Guid OrderId)
        {
            return await _context.Orders.Include(o => o.OrderItems).
                ThenInclude(o => o.Product).FirstOrDefaultAsync(o => o.Id == OrderId);
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o=>o.Payments)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .OrderByDescending(o=>o.CreatedAt)
                .ToListAsync();
        }
        public async Task<List<Order>> GetAllFailedOrders()
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.Payments)
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .Where(o=>o.Status==Enums.OrderStatus.Cancelled)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

       
    }
}
