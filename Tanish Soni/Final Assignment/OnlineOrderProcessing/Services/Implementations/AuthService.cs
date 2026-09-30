using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using OnlineOrderProcessing.Models;
using OnlineOrderProcessing.Repositories;
using OnlineOrderProcessing.ViewModels;
using System.Security.Claims;

namespace OnlineOrderProcessing.Services.Implementations
{
    public class AuthService : IAuthServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthRepository _authRepository;

        public AuthService(SignInManager<ApplicationUser> signInManager , IAuthRepository authRepository,IHttpContextAccessor httpContextAccessor)
        {
            _signInManager = signInManager;
            _httpContextAccessor= httpContextAccessor;
            _authRepository = authRepository;
        }

        public async Task<Result> Login (LoginViewModel model)
        {

            var user = await _authRepository.GetUserByEmail(model.Email);

            if (user == null)
            {
                return new Result
                {
                    Success = false,
                    Message = "User not registered"
                };

            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (!result.Succeeded)
            {
                return new Result
                {
                    Success = false,
                    Message = "Password not Valid"
                };
            }
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.Email,
                    user.Email
                ),

                new Claim(
                    "FullName",
                    user.UserName
                ),

                new Claim(
                    "UserId",
                    user.Id.ToString()
                )
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var claimsPrincipal = new ClaimsPrincipal(
                claimsIdentity
            );

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                claimsPrincipal,
                authProperties
            );

            return new Result { Success= true,
            Message= "Login Succesfully !"};






        }
    }
}
