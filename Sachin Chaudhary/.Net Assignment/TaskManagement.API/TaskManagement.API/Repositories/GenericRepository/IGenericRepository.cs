using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace TaskManagement.API.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);

        Task<List<T>> FindAsync1(
          Expression<Func<T, bool>> predicate);
        Task CreateAsync(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void  Delete(T entity);   
    }
}
