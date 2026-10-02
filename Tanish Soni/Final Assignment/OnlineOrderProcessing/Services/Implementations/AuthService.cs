using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class AuthService : IAuthServices
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthRepository _authRepository;

        public AuthService(
            SignInManager<ApplicationUser> signInManager,
            IAuthRepository authRepository)
        {
            _signInManager = signInManager;
            _authRepository = authRepository;
        }

        public async Task<Result> Login(LoginViewModel model)
        {
            var user = await _authRepository
                .GetUserByEmail(model.Email);

            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "User not registered"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                false);

            if (!result.Succeeded)
            {
                return new Result
                {
                    Success = false,
                    Message = "Password not valid"
                };
            }

            await _signInManager.SignInAsync(
                user,
                isPersistent: true);

            return new Result
            {
                Success = true,
                Message = "Login Successfully!"
            };
        }
    }
}