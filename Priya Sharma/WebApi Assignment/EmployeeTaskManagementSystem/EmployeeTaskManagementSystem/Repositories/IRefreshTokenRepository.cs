using TaskManagementSystemApi.Models;

namespace TaskManagementSystemApi.Repositories
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task<RefreshToken?> GetActiveTokenAsync(string token);
    }
}
