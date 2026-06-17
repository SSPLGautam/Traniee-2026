using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeTaskManagementAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetById(int id)
        {
            return await _appDbContext.Set<T>().FindAsync(id);
        }


        public async void DeleteAsync(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
        }

        public async void UpdateAsync(T entity)
        {

            _appDbContext.Set<T>().Update(entity);
        }
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().Where(predicate).ToListAsync();
        }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
    
    }
}
