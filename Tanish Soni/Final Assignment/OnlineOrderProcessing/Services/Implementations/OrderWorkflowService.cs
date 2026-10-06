using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class OrderWorkflowService :IOrderWorkflowService
    {
        public bool CanChange(OrderStatus current, OrderStatus next)
        {
            if (current == OrderStatus.Pending)
            {
                return next == OrderStatus.Paid ||
                       next == OrderStatus.Cancelled;
            }

            if (current == OrderStatus.Paid)
            {
                return next == OrderStatus.Processing ||
                       next == OrderStatus.Cancelled;
            }

            if (current == OrderStatus.Processing)
            {
                return next == OrderStatus.Shipped ||
                       next == OrderStatus.Cancelled;
            }

            if (current == OrderStatus.Shipped)
            {
                return next == OrderStatus.Delivered;
            }

            return false;
        }
    }
}
