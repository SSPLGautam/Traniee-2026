using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface ICartService
    {
        Task<CartItemListViewModel> GetCartItemsListByUserId();
        Task<Result> AddToCart( Guid productId);
        Task<Result> RemoveItem(Guid cartItemId);
        Task<Result> UpdateItem(Guid cartItemId, int change);
    }
}
