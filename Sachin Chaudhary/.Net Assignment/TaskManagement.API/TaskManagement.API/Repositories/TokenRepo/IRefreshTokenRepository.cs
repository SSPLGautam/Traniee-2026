using TaskManagement.API.Models;
using TaskManagement.API.Repositories.GenericRepository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
}