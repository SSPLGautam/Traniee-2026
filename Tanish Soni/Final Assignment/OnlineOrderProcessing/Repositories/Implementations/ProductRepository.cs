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
        public async Task<List<Product>> GetAllProductsAsync(
      string? search,
      int page,
      int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(q =>
                    q.Name.Contains(search) ||
                    q.SKU.Contains(search));
            }

            var products = await query
                .OrderBy(x => x.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return products;
        }
        public async Task<int> GetProductCountAsync(string? search)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(q =>
                    q.Name.Contains(search) ||
                    q.SKU.Contains(search));
            }

            return await query.CountAsync();
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
