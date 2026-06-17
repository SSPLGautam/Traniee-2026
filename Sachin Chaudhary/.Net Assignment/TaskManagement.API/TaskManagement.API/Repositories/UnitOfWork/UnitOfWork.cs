using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Models;
using TaskManagement.API.Repositories.GenericRepository;
using TaskManagement.API.Repositories.Taskrepo;

namespace TaskManagement.API.Repositories.UnitOfWork.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public ITaskRepository Tasks { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public UnitOfWork(ApplicationDbContext _context)
        {
            context = _context;

            Tasks = new TaskRepository(context);
            RefreshTokens = new RefreshTokenRepository(context);

        }
        public async Task<int> SaveChangesAsync()
        {


            return await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
