using OnlineOrderProcessing.Enums;

namespace OnlineOrderProcessing.Services
{
    public interface IOrderWorkflowService
    {
        bool CanChange(OrderStatus current, OrderStatus next);
    }
}
