
using EmployeeTaskManagementAPI.Data;
using EmployeeTaskManagementAPI.Dto;
using EmployeeTaskManagementAPI.IRepository;
using EmployeeTaskManagementAPI.IService;
using EmployeeTaskManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTaskManagementAPI.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public RefreshTokenService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<string> LogoutAsync(RefreshTokenDto refreshTokenDto)
        {
            
                var refreshToken = await _unitOfWork.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshTokenDto.RefreshToken && x.IsActive);

                if (refreshToken == null)
                {
                    return "Invalid token";
                }

                refreshToken.IsActive = false;
                refreshToken.RevokedAt = DateTime.UtcNow;

                await _unitOfWork.SaveAsync();
                return "Logout successful";
            }
           
        


        public async Task SaveRefreshTokenAsync(string userId, string token)
        {
            
                var refreshToken = new RefreshToken
                {
                    UserId = userId,
                    Token = token,
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(1),
                    IsActive = true
                };

                await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveAsync();



        }

        public async Task<TokenResponseDto> RotateRefreshToken(string token)
        {
            var oldRefreshToken = await _unitOfWork.RefreshTokens.FirstOrDefaultAsync(x => x.Token == token && x.IsActive);
            if(oldRefreshToken == null)
            {
                throw new Exception("invalid token");
            }
            if(oldRefreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new Exception("Token expired");
            }
            oldRefreshToken.IsActive = false;
            oldRefreshToken.RevokedAt = DateTime.UtcNow;
            var user = await _userManager.FindByIdAsync(oldRefreshToken.UserId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var role = await _userManager.GetRolesAsync(user);
            var accessToken = await _tokenService.GenerateAccessTokenAsync(user);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Token = newRefreshToken,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddMinutes(1),
                IsActive = true
            };
            await _unitOfWork.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.SaveAsync();
            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = 1800
            };
        }
    }
}
