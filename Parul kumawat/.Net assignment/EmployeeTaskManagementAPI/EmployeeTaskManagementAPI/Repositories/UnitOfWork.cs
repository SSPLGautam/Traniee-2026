using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.IRepository;
using EmployeeTaskManagementAPI.Models;

namespace EmployeeTaskManagementAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Tasks> Tasks { get; }
        public IGenericRepository<RefreshToken> RefreshTokens { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Tasks = new GenericRepository<Tasks>(_context);
            RefreshTokens = new GenericRepository<RefreshToken>(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
