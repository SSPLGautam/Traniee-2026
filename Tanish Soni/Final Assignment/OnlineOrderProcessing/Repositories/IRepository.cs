namespace OnlineOrderProcessing.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid Id);

        Task AddAsync(T entity);

        void Update(T entity);
        Task<bool> ExistsAsync(Guid id);


        void Delete(T entity);
      
    }
}
