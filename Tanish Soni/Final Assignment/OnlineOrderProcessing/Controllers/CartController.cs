using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Services;
using System.Security.Claims;

namespace OnlineOrderProcessing.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
         
            var Model = await _cartService.GetCartItemsListByUserId();

            return View(Model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            var result = await _cartService.AddToCart( productId);
           
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid cartItemId)
        {
            var result = await _cartService.RemoveItem(cartItemId);

            if (!result.Success)
            {
                return Json(result);
            }

            var vm = await _cartService.GetCartItemsListByUserId();

            return PartialView("_CartContent", vm);
        }
        [HttpPost]

        public async Task<IActionResult> Update(Guid cartItemId, int change)
        {
            var result = await _cartService.UpdateItem(cartItemId, change);
          
            if (!result.Success)
            {
                return Json(result);
            }
            var vm = await _cartService.GetCartItemsListByUserId();

            return PartialView("_CartContent", vm);
        }


    }
}
