using Microsoft.EntityFrameworkCore.Storage;
using OnlineOrderProcessing.Data;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public  IProductRepository Products { get; }

        public ICartRepository CartItem { get; }

        public   IOrderRepository Order { get; }

      public  IOrderEventRepository OrderEvent { get; }

        public IOrderItemRepository OrderItem { get; }

      public   IPayementRepository Payment { get; }


        private IDbContextTransaction? _transaction;
        public UnitOfWork (ApplicationDbContext context)
        {
            _context = context;
            Products = new ProductRepository(_context);
            CartItem = new CartRepository(_context);
            Order = new OrderRepository(_context);
            OrderItem = new OrderItemRepository(_context);
            OrderEvent = new OrderEventRepository(_context);
            Payment = new PaymentRepository(_context);
           
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();

            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
