namespace FinTrackApi.Services
{
    using FinTrackApi.Models.RequestModels;
    using FinTrackApi.Models.ResponseModels;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string username, string role, string secret);

        Task<bool> Register(RegisterModel reuqestModel);

        Task<LoginResponseModel> Login(LoginModel loginModel);

    }
}
