using EmployeeTaskManagementAPI.Models;

namespace EmployeeTaskManagementAPI.IRepository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Tasks> Tasks { get; }
        IGenericRepository<RefreshToken> RefreshTokens { get; }

        Task<int> SaveAsync();
    }
}
