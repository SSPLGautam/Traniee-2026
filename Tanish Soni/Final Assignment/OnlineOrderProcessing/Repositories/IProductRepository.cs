
using OnlineOrderProcessing.Models;

namespace OnlineOrderProcessing.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetAllProductsAsync(string ? Search );

        Task<bool> TryDecreaseStockAsync(Guid productId, int quantity);

    }
}
