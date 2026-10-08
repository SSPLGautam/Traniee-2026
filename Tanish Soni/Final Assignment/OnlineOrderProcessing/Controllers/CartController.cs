using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineOrderProcessing.Services;
using System.Security.Claims;

namespace OnlineOrderProcessing.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
         

            var result = await _cartService.GetCartItemsListByUserId();

            return View(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid productId)
        {
            var result = await _cartService.AddToCart( productId);

            if (result.IsFailure)
            {
              return  Json(new
                {
                    success = false,
                    Message = result.ErrorMessage
                });
            }
           
            return Json(
                new
                {
                    success = true,
                    Message = "Add to Cart Successfully !"
                } );
        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid cartItemId)
        {
            var result = await _cartService.RemoveItem(cartItemId);

            if (result.IsFailure)
            {
                return Json(new { Success=false, message= result.ErrorMessage  });
            }

            var vm = await _cartService.GetCartItemsListByUserId();

            return PartialView("_CartContent", vm.Value);
        }
        [HttpPost]

        public async Task<IActionResult> Update(Guid cartItemId, int change)
        {
            var result = await _cartService.UpdateItem(cartItemId, change);

            if (result.IsFailure)
            {
                return Json(new { Success = false, message = result.ErrorMessage });
            }

            var cartResult =
       await _cartService.GetCartItemsListByUserId();

            if (cartResult.IsFailure)
            {
                return Json(new
                {
                    success = false,
                    message = cartResult.ErrorMessage
                });
            }


            return PartialView("_CartContent", cartResult.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var result = await _cartService.GetCartItemCount();

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
                count = result.Value
            });
        }
    }

}
