using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class CartRepository:GenericRepository<CartItem>,ICartRepository

    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CartItemViewModel>> GetAllCartItemByUserId(string userId)
        {
            var cartItems= await _context.CartItems
                .Include(c=>c.Product)
                .Where(c => c.UserId == userId)
                .Select(c=> new CartItemViewModel { 
                  Id = c.Id,
                  Quantity=c.Quantity,
                  Product=c.Product,
                  Price=c.Quantity*c.Product.Price
                  
                })

                .ToListAsync();

            return cartItems;

        }
        public async Task<CartItem> GetCartItem(string userId, Guid productId)
        {
            return _context.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }
    }
}