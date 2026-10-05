using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IPaymentService
    {
        Task<PaymentPageViewModel> GetPaymentPage(Guid OrderId);
        Task<Result> Pay(Guid OrderId);
    }
}
