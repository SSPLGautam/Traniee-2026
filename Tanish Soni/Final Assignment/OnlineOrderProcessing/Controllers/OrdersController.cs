using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Services;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Controllers
{
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
    }
}
