using OnlineOrderProcessing.Common;
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
        public async Task<Result<CartItemListViewModel>> GetCartItemsListByUserId()
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<CartItemListViewModel>.Failure("User Not Found");
            }
            var cartItems = await _unitOfWork.CartItem.GetAllCartItemByUserId(userId);

            var subTotal = cartItems.Sum(c => c.Price);

            var totalItem = cartItems.Count();

            return Result<CartItemListViewModel>.Success (new CartItemListViewModel
            {
                CartItems = cartItems.ToList(),
                SubTotal = subTotal,
                TotalItem = totalItem

            });
        }
        public async Task<Result<bool>> AddToCart(Guid productId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId==null)
            {
                return Result<bool>.Failure("User not Found");

            }
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product.Stock<1)
            {
                return Result<bool>.Failure($"{product.Name} is  out of stock  ");
               
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
                return Result<bool>.Failure("Uable to Add to Cart !");
              
            }
            return Result < bool>.Success(true);
           

        }


        public async Task<Result<bool>> RemoveItem(Guid cartItemId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Result<bool>.Failure("User not found");
               
            }
            var cartItem = await _unitOfWork.CartItem.GetByIdAsync(cartItemId);
            if (cartItem == null)
            {
                return Result<bool>.Failure("Cart items not found");


            }
            _unitOfWork.CartItem.Delete(cartItem);

            int result = await _unitOfWork.SaveChangesAsync();

            if (result <= 0)
            {
                return Result<bool>.Failure("Unable to Remove");
            }

            return Result<bool>.Success(true);


        }

        public async Task<Result<bool>> UpdateItem(Guid cartItemId , int change)
        {
            var item = await _unitOfWork.CartItem.GetByIdAsync(cartItemId);
            if(item == null)
            {
                  return Result<bool>.Failure("Cart item not found");
            }
           var newQuantity=  item.Quantity + change;

            if (newQuantity == 0)
            {
                return Result<bool>.Failure("Quantity cannot be less than 0 !");
            }
        
            item.Quantity = newQuantity;
            var result=  await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);


        }
        public async Task<Result<int>> GetCartItemCount()
        {
            var userId = _httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Result<int>.Failure("User not logged in.");
            }

            var count = await _unitOfWork.CartItem
                .GetCartItemCountAsync(userId);

            return Result<int>.Success(count);
        }
    }
}