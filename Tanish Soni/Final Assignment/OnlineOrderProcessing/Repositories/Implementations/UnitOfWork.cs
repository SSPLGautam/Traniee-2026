using OnlineOrderProcessing.Data;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public  IProductRepository Products { get; }

        public ICartRepository CartItem { get; }

             
        public UnitOfWork (ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            CartItem = new CartRepository(_context);
           
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        }
}
