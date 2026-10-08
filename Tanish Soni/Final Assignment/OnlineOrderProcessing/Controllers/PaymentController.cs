using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Services;

namespace OnlineOrderProcessing.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {


        private readonly IPaymentService _paymentService;

        public PaymentController (IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet("/Payment/{orderId:guid}")]
        public async Task<IActionResult> Index(Guid orderId)
        {
            var result = await _paymentService.GetPaymentPage(orderId);
            return View(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(Guid orderId)
        {
            var result= await _paymentService.Pay(orderId);

            if (result.IsFailure)
            {
                return Json(new
                {
                    success = false,
                    message = result.ErrorMessage
                });
            }

            return Json(new
            {
                success = true,
                message = "Payment successful"
            });


        }


    }
}
