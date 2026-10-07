using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories
{
    public interface IAuthRepository
    {
        Task<ApplicationUser> GetUserByEmail(string email);
        Task<List<string>> GetUserRolesByUser(ApplicationUser user);

        Task<IdentityResult> CreateUserAsync(
            ApplicationUser user,
            string password);

        Task<IdentityResult> AddUserToRoleAsync(
            ApplicationUser user,
            string role);
    }
}
