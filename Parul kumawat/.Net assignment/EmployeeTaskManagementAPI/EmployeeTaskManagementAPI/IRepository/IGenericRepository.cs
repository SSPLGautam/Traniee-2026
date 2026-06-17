using System.Linq.Expressions;

namespace EmployeeTaskManagementAPI.IRepository
{
    public interface IGenericRepository<T> where T : class
    {

        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        Task<T> GetById(int id);
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    
    }
}
