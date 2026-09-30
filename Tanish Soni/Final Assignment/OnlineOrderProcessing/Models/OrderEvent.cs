using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.Models
{
    public class OrderEvent
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public OrderEventType EventType { get; set; }

        public string? Details { get; set; } 

        public DateTime CreatedAt { get; set; }

        public Order Order { get; set; } = null!;
    }
}
