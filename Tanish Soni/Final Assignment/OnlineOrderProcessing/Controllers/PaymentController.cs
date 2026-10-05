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
            var model = await _paymentService.GetPaymentPage(orderId);
            return View(model);
        }

        [HttpGet("/Pay/{orderId:guid}")]
        public async Task<IActionResult> Pay(Guid orderId)
        {
            var model = await _paymentService.Pay(orderId);
            
            return Json(model);


        }


    }
}
