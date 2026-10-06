using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Enums;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController (IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        [Route("/Orders")]
        public async Task<IActionResult> Index()

        {
            var model =await _orderService.GetOrders();
            if (model==null)
            {
                return NotFound();
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AOrders()

        {
            var model = await _orderService.GetAllOrders();
           
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> FailedOrders()

        {
            var model = await _orderService.GetAllFailedOrders();

            return View(model);
        }


        [HttpPost]  
        [Route("/api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateOrderViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Result
                {
                    Success = false,
                    Message = "Invalid order request."
                    
                });

            }
            var result = await _orderService.Create(Model);
            return Json(result);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(
          Guid orderId,
          OrderStatus status)
        {
            var result = await _orderService.UpdateOrderStatus(
                orderId,
                status);
            TempData["Message"] = result.Message;
            return RedirectToAction("AOrders");
        }


    }
}
