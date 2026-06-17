using TaskManagement.API.Models;
using TaskManagement.API.Repositories.GenericRepository;
using TaskManagement.API.Repositories.Taskrepo;

namespace TaskManagement.API.Repositories.UnitOfWork.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        ITaskRepository Tasks { get; }
        IRefreshTokenRepository RefreshTokens { get; }

        Task<int> SaveChangesAsync();

    }
}
