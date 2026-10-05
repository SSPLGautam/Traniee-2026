using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories
{
    public interface IPayementRepository : IRepository<Payment>
    {
        Task<Payment> GetPaymentByOrderId(Guid orderId);
        Task<PaymentResult> Pay();
    }
}
