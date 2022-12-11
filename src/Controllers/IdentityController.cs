
namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels;
    using FinTrackApi.Models.ResponseModels;
    using FinTrackApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<bool> Regiter(RegisterModel registerRequestModel)
        {
            var register = await identityService.Register(registerRequestModel);
            return register;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<LoginResponseModel> Login(LoginModel model)
        {
            var login = await identityService.Login(model);
            return login;
        }
    }
}
