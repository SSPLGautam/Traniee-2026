using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class AdminOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerEmail { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public decimal TotalAmount { get; set; }

        public OnlineOrderProcessing.Enums.OrderStatus Status { get; set; } 

        public int PaymentAttempts { get; set; }

        public List<OrderItems> Items { get; set; } = new();
    }
}
