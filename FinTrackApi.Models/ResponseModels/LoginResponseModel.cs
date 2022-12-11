
namespace FinTrackApi.Models.ResponseModels
{
    public class LoginResponseModel
    {
        public required string Username {get; set;}

        public required string JwtToken {get; set;}

    }
}
