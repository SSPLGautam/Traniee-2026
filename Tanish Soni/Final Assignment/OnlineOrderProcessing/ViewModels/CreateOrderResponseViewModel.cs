using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.ViewModels
{
    public class CreateOrderResponseViewModel 
    {
      public   Guid  OrderId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
