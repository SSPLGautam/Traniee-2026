
namespace OnlineOrderProcessing.Repositories
{
    public interface IUnitOfWork
    {

        IProductRepository Products { get; }
        ICartRepository CartItem { get; }
        Task<int> SaveChangesAsync();

    
    }
}
