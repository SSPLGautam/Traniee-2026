using OnlineOrderProcessing.ViewModels;

namespace OnlineOrderProcessing.Services
{
    public interface IAuthServices
    {
        Task<Result> Login(LoginViewModel Model);
    }
}
