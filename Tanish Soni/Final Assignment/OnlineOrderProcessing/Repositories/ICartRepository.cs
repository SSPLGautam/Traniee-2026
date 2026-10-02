using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories
{
    public interface ICartRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItemViewModel>> GetAllCartItemByUserId(string userId);
        Task<CartItem> GetCartItem(string userId, Guid productId);
    }
}
