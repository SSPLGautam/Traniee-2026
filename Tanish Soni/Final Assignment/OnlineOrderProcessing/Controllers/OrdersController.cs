using Azure;
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
            var result =await _orderService.GetOrders();
            if (result.Value==null)
            {
                return NotFound();
            }
            return View(result.Value);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> AdminOrders(int page=1)

        {
            if (page < 1)
            {
                page = 1;
            }

            const int pageSize = 3;

            var result = await _orderService.GetAllOrders(page, pageSize);

            if (result.IsFailure)
            {
                TempData["Message"] = result.ErrorMessage;
                return View(new AdminOrdersViewModel());
            }

            return View(result.Value);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> FailedOrders()

        {
            var model = await _orderService.GetAllFailedOrders();

            return View(model.Value);
        }


        [HttpPost]  
        [Route("/api/[controller]")]
        public async Task<IActionResult> Create([FromBody] CreateOrderViewModel Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new 
                {
                    Success = false,
                    Message = "Invalid order request."
                    
                });

            }
            var result = await _orderService.Create(Model);

            return Json(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(
          Guid orderId,
          OrderStatus status)
        {
            var result = await _orderService.UpdateOrderStatus(
                orderId,
                status);
            if (result.IsFailure)
            {
                TempData["Message"] = result.ErrorMessage;
            }
            return RedirectToAction("AdminOrders");
        }


    }
}
