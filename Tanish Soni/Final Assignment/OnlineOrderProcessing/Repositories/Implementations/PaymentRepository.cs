using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class PaymentRepository : GenericRepository<Payment>, IPayementRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
