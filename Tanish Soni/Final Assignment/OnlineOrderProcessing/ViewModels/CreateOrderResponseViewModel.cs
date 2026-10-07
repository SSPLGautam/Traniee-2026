using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.ViewModels
{
    public class CreateOrderResponseViewModel : Result
    {
      public   Guid  OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
