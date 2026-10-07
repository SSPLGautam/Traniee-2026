using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class PaymentPageViewModel
    {
        public Guid OrderId { get; set; }

        public List<OrderItems> Items { get; set; }

        public decimal TotalAmmount { get; set; }

        



    }
}
