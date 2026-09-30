using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByEmail(string email);
    }
}
