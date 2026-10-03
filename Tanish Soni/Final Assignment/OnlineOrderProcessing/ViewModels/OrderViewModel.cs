using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        public int TotalItems { get; set; }

        public List<OrderItems> Items { get; set; }
    }
}
