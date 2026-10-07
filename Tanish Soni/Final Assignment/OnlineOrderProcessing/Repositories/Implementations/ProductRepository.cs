using OnlineOrderProcessing.Data;
using OnlineOrderProcessing.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class ProductRepository : GenericRepository<Product> , IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProductsAsync(string? Search)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = query.Where(q => q.Name.Contains(Search) || q.SKU.Contains(Search));
            }

                var products = await query.ToListAsync();

            return products;
        }

        public async Task<bool> TryDecreaseStockAsync(Guid productId, int quantity)
        {
            var rows = await _context.Products
                .Where(p => p.Id == productId && p.Stock >= quantity)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Stock, p => p.Stock - quantity));

            return rows == 1;
        }
    }
}
