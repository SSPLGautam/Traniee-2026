using TaskManagementSystemApi.Data;
using TaskManagementSystemApi.Repositories.Implementation;

namespace TaskManagementSystemApi.Repositories.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ITaskRepository Tasks { get; }
        public IRefreshTokenRepository RefreshTokens { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Tasks = new TaskRepository(_context);
            RefreshTokens = new RefreshTokenRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
