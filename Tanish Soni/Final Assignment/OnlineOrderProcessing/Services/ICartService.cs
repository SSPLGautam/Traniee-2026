using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface ICartService
    {
        Task<Result< CartItemListViewModel>> GetCartItemsListByUserId();
        Task<Result<bool>> AddToCart( Guid productId);
        Task<Result<bool>> RemoveItem(Guid cartItemId);
        Task<Result<bool>> UpdateItem(Guid cartItemId, int change);
        Task<Result<int>> GetCartItemCount();
    }
}
