using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public OrderStatus Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderRequestKey { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;

        public ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();


        public ICollection<OrderEvent> OrderEvents { get; set; } = new List<OrderEvent>();


    }
}
