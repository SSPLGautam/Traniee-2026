using OnlineOrderProcessing.Common;
using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IAuthServices
    {
        Task<Result<bool>> Login(LoginViewModel Model);
        Task<Result<bool>> Register(RegisterViewModel model);
        Task Logout();
    }
}
