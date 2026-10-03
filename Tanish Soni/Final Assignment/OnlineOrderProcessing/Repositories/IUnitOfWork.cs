
using Microsoft.EntityFrameworkCore.Storage;

namespace OnlineOrderProcessing.Repositories
{
    public interface IUnitOfWork
    {

        IProductRepository Products { get; }
        ICartRepository CartItem { get; }

        IOrderRepository Order { get; }

        IOrderEventRepository OrderEvent { get; }

        IOrderItemRepository OrderItem { get; }

        IPayementRepository Payment { get; }


        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();


    }
}
