using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;
using System.Security.Claims;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CartItemListViewModel> GetCartItemsListByUserId()
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new CartItemListViewModel();
            }
            var cartItems = await _unitOfWork.CartItem.GetAllCartItemByUserId(userId);

            var subTotal = cartItems.Sum(c => c.Price);

            var totalItem = cartItems.Count();

            return new CartItemListViewModel
            {
                CartItems = cartItems.ToList(),
                SubTotal = subTotal,
                TotalItem = totalItem

            };
        }
        public async Task<Result> AddToCart(Guid productId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId==null)
            {
                return new Result { 
                 Success=false,
                 Message="User not exist"
                };

            }
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product.Stock<1)
            {
                return new Result
                {
                    Success = false,
                    Message = $"{product.Name } is  out of stock  "
                };
            }
            var existingCartItem = await _unitOfWork.CartItem.GetCartItem(userId, productId);
            int result;
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;

            }
            else
            {

                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    ProductId = productId,
                    Quantity = 1

                };

                await _unitOfWork.CartItem.AddAsync(cartItem);
            }
            result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "Uable to Add to Cart !"
                };
            }
            return new Result
            {
                Success = true,
                Message = "Add To Cart Successfully ! "
            };

        }


        public async Task<Result> RemoveItem(Guid cartItemId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "User not exist"
                };

            }
            var cartItem = await _unitOfWork.CartItem.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "Cart Item is not found !"
                };


            }
            _unitOfWork.CartItem.Delete(cartItem);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "Uable to Remove  !"
                };
            }
            return new Result
            {
                Success = true,
                Message = "Remove Successfully ! "
            };


        }

        public async Task<Result> UpdateItem(Guid cartItemId , int change)
        {
            var item = await _unitOfWork.CartItem.GetByIdAsync(cartItemId);
            if(item == null)
            {
                  return new Result
                {
                    Success = false,
                    Message = "Cart Item is not found !"
                };
            }
           var newQuantity=  item.Quantity + change;

            if (newQuantity == 0)
            {
                return new Result
                {
                    Success = false,
                    Message = "Quantity cannot be less than 0 !"
                };
            }
        
            item.Quantity = newQuantity;
            var result=  await _unitOfWork.SaveChangesAsync();

            return new Result
            {
                Success = result > 0,
                Message = item.Quantity.ToString()
            };
                

        }
    }
}