using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class OrderItemRepository : GenericRepository<OrderItems> , IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
