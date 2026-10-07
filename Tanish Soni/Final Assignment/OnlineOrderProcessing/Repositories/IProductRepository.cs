
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllProductsAsync(
    string? search,
    int page,
    int pageSize);

        Task<int> GetProductCountAsync(string? search);

        Task<bool> TryDecreaseStockAsync(Guid productId, int quantity);

    }
}
