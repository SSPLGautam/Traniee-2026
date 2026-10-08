using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class PaymentServices :IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PaymentServices(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<Result< PaymentPageViewModel>> GetPaymentPage(Guid OrderId)
        {
            var order = await _unitOfWork.Order.GetOrderById(OrderId);
           if(order== null)
            {
                return null;
            }
            return Result<PaymentPageViewModel>.Success( new PaymentPageViewModel
            {
                OrderId = order.Id,
                Items = order.OrderItems.ToList(),
                TotalAmmount = order.TotalAmount


            });
            
        }

        public async Task<Result<bool>> Pay (Guid OrderId)
        {
            var order = await _unitOfWork.Order.GetOrderById(OrderId);
            if (order.Status != OrderStatus.Pending)
            {
                return Result<bool>.Failure("This order cannot be paid.");
            }

            if (order == null)
            {
                return Result<bool>.Failure("User not found");


            }
            var lastPayment = await _unitOfWork.Payment.GetPaymentByOrderId(OrderId);


            var attempt = lastPayment == null ? 1 : lastPayment.Attempt + 1;
            if (lastPayment?.Attempt > 3)
            {
                return Result<bool>.Failure("Payment Attempt is completed , Please make a new Order");
            }


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var orderStartEvent = new OrderEvent {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    Details = "Payment Started",
                    EventType = OrderEventType.PaymentStarted,
                    OrderId = OrderId,
                };


                await _unitOfWork.OrderEvent.AddAsync(orderStartEvent);

                var result = await _unitOfWork.Payment.Pay();

                var payment = new Payment
                {
                    Id = Guid.NewGuid(),
                    Attempt = attempt,
                    CreatedAt = DateTime.Now,
                    Message = "payment made",
                    OrderId = OrderId,
                    Result = result,
                };
                await _unitOfWork.Payment.AddAsync(payment);

                if (result == PaymentResult.Success)
                {
                    order.Status = OrderStatus.Paid;

                    var orderEvent = new OrderEvent
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        Details = "Payment succesfully",
                        EventType = OrderEventType.PaymentSucceeded,
                        OrderId = OrderId,
                    };
                   await  _unitOfWork.OrderEvent.AddAsync(orderEvent);

                        _unitOfWork.Order.Update(order);
                    await _unitOfWork.SaveChangesAsync();
                    await _unitOfWork.CommitTransactionAsync();

                    return Result<bool>.Success(true);
                }

                var failedEvent = new OrderEvent
                {
                    Id = Guid.NewGuid(),

                    OrderId = OrderId,

                    CreatedAt = DateTime.UtcNow,

                    Details =
                         $"Payment {result} on attempt {attempt}",

                    EventType =
                         OrderEventType.PaymentFailed
                };

                await _unitOfWork.OrderEvent.AddAsync(failedEvent);

                if (attempt >= 3)
                {
                    order.Status = OrderStatus.Cancelled;

                    _unitOfWork.Order.Update(order);

                    foreach (var item in order.OrderItems)
                    {
                        item.Product.Stock += item.Quantity;
                    }
                    var cancelledEvent = new OrderEvent
                    {
                        Id = Guid.NewGuid(),

                        OrderId = OrderId,

                        CreatedAt = DateTime.UtcNow,

                        Details =
                            "Order cancelled after 3 failed payment attempts.",

                        EventType = OrderEventType.OrderCancelled
                    };

                    await _unitOfWork.OrderEvent
                        .AddAsync(cancelledEvent);




                    await _unitOfWork.SaveChangesAsync();

                    await _unitOfWork.CommitTransactionAsync();


                    return Result<bool>.Failure("Payment failed 3 times. Order has been cancelled.");


                }

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();


                return Result<bool>.Failure("Payment Failed");

            }
            catch (Exception ex) {
                await _unitOfWork.RollbackTransactionAsync();
                return Result<bool>.Failure("Unexpected error");

            }


        }
    }
}
