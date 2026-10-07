using AspNetCoreGeneratedDocument;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Enums;
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
        public async Task<Payment> GetPaymentByOrderId(Guid orderId)
        {
            return  _context.Payments.OrderByDescending(o=>o.CreatedAt).FirstOrDefault(p => p.OrderId == orderId);
        }
        public async Task<PaymentResult> Pay()
        {
            int randomNumber = Random.Shared.Next(1, 101);
            await Task.Delay(2000);
            if (randomNumber <= 60) {
                return PaymentResult.Success
                    ;
            }
            else if (randomNumber <= 90)
            {
                return PaymentResult.Failed;
            }

            return PaymentResult.Timeout;
        }
    }
}
