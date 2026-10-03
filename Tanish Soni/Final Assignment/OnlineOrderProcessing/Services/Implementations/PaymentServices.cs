using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class PaymentServices :IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentServices(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<PaymentPageViewModel> GetPaymentPage(Guid OrderId)
        {
            var order = await _unitOfWork.Order.GetOrderById(OrderId);
            return new PaymentPageViewModel
            {
                OrderId = order.Id,
                Items = order.OrderItems.ToList(),
                TotalAmmount = order.TotalAmount


            };
            
        }

    }
}
