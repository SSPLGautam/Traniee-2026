namespace OnlineOrderProcessing.Enums
{
    public enum OrderEventType
    {
        OrderCreated,
        PaymentStarted,
        StockReleased,
        PaymentSucceeded,
        PaymentFailed,
        OrderConfirmed,
        OrderShipped,
        OrderDelivered,
        OrderCancelled
    }
}
