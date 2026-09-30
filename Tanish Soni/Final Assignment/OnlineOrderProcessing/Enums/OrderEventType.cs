namespace OnlineOrderProcessing.Enums
{
    public enum OrderEventType
    {
        OrderCreated,
        PaymentStarted,
        PaymentSucceeded,
        PaymentFailed,
        OrderConfirmed,
        OrderShipped,
        OrderDelivered,
        OrderCancelled
    }
}
