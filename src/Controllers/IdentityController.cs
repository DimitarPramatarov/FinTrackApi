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
        [Route("register")]
        public async Task<ActionResult> Regiter(RegisterModel registerRequestModel)
        {
            var register = await identityService.Register(registerRequestModel);

            if(register == false)
            {
                return Unauthorized(registerRequestModel);
            }

            return Ok(register);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginModel model)
        {
            var login = await identityService.Login(model);

            if(!string.IsNullOrEmpty(login.JwtToken))
            {
                return Ok(login);
            }

            return Unauthorized(login);
        }
    }
}
