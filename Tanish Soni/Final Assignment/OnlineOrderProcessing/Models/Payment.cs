using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.Models
{
    public class Payment
    {
       public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public int Attempt { get; set; }

        public PaymentResult Result { get; set; }

        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order Order { get; set; } = null!;
    }
}
