using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Repositories.Implementations
{
    public  class AuthRepository:IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> GetUserByEmail(string  email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
        public async Task<List<string>> GetUserRolesByUser(ApplicationUser user)
        {
            
            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }
        public async Task<IdentityResult> CreateUserAsync(
           ApplicationUser user,
           string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserToRoleAsync(
            ApplicationUser user,
            string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }
    }
}
