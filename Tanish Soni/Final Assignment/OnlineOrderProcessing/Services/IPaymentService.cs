using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IPaymentService
    {
        Task<Result< PaymentPageViewModel>> GetPaymentPage(Guid OrderId);
        Task<Result<bool>> Pay(Guid OrderId);
    }
}
