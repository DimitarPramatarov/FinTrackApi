namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;
    using FinTrackApi.Services.BalanceService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BalanceController : ApiController
    {
        private readonly IBalanceService balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            this.balanceService = balanceService;
        }

        [HttpPost]
        public async Task<ActionResult<BalanceResponseModel>> GetAccountBalance(RequestByIdModel id)
        {
          
           var balance = await this.balanceService.GetAccountBalance(id);
           if (balance != null)
           {
               return Ok(balance);
           }

            return Unauthorized(balance);
        }
    }
}
