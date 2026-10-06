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

        public async Task<PaymentPageViewModel> GetPaymentPage(Guid OrderId)
        {
            var order = await _unitOfWork.Order.GetOrderById(OrderId);
           if(order== null)
            {
                return null;
            }
            return new PaymentPageViewModel
            {
                OrderId = order.Id,
                Items = order.OrderItems.ToList(),
                TotalAmmount = order.TotalAmount


            };
            
        }

        public async Task<Result> Pay (Guid OrderId)
        {
            var order = await _unitOfWork.Order.GetOrderById(OrderId);
            if (order.Status != OrderStatus.Pending)
            {
                return new Result
                {
                    Success = false,
                    Message = "This order cannot be paid."
                };
            }

            if (order == null)
            {
                return new Result
                {
                    Success =false,
                    Message="User not found"
                };


            }
            var lastPayment = await _unitOfWork.Payment.GetPaymentByOrderId(OrderId);


            var attempt = lastPayment == null ? 1 : lastPayment.Attempt + 1;
            if (lastPayment?.Attempt > 3)
            {
                return new Result
                {
                    Success = false,
                    Message = "Payment Attempt is completed , Please make a new Order"
                };
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

                    return new Result
                    {
                        Success = true,
                        Message = "Payment Successfully"
                    };
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


                    return new Result
                    {
                        Success = false,

                        Message =
                            "Payment failed 3 times. Order has been cancelled."
                    };


                }

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();


                return new Result
                {
                    Success = false,

                    Message = "Payment Failed"
                };

            }
            catch (Exception ex) {
                await _unitOfWork.RollbackTransactionAsync();
                return new Result
                {
                    Success = false,

                    Message ="Unexpected error"
                };

            }


        }
    }
}
