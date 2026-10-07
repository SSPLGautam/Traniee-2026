using Microsoft.EntityFrameworkCore;
using OnlineOrderProcessing.Data;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<T?> GetByIdAsync(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }
        public async Task AddAsync(T entity)
        {

            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {

            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<bool> ExistsAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);

            return entity != null;
        }

    }
}
