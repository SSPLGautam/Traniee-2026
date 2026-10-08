using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Common;
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

        public async Task<Result<bool>> Login(LoginViewModel model)
        {
            var user = await _authRepository
                .GetUserByEmail(model.Email);

            if (user == null)
            {
                return Result<bool>.Failure("User not registered");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(
                user,
                model.Password,
                false);

            if (!result.Succeeded)
            {
                return  Result<bool>.Failure("Password Invalid");
            }

            await _signInManager.SignInAsync(
                user,
                isPersistent: true);

            return Result<bool>.Success(true);
        }
           
         public async Task Logout()
        {
             await _signInManager.SignOutAsync();
        }
        public async Task<Result<bool>> Register(RegisterViewModel model)
        {
            var existingUser =
                await _authRepository.GetUserByEmail(model.Email);

            if (existingUser != null)
            {
                return Result<bool>.Failure("Email is already registered.");
            }

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
              
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _authRepository.CreateUserAsync(
                user,
                model.Password);

            if (!result.Succeeded)
            {


                return Result<bool>.Failure("UnSuccessful Registration");
            }

            var roleResult =
                await _authRepository.AddUserToRoleAsync(
                    user,
                    "Customer");

            if (!roleResult.Succeeded)
            {

                return Result<bool>.Failure("Unsuccessful Registration");
            }
            await _signInManager.SignInAsync(
               user,
               isPersistent: true);

            return Result<bool>.Success(true);
        }
    }
}