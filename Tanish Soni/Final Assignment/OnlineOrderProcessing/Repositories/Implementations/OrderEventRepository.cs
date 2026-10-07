using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class OrderEventRepository :  GenericRepository<OrderEvent>,IOrderEventRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderEventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
