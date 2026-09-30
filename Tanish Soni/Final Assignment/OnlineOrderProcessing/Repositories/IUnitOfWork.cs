
namespace OnlineOrderProcessing.Repositories
{
    public interface IUnitOfWork
    {

        IProductRepository Products { get; }
        Task<int> SaveChangesAsync();

    
    }
}
