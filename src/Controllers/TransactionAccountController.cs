namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;
    using FinTrackApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore.Scaffolding;

    [Authorize]
    public class TransactionAccountController : ApiController    
    {
        private readonly ITransactionAccountService transactionService;

        public TransactionAccountController(ITransactionAccountService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        [Route("/transaction-accounts/my-accounts")]
        public async Task<ActionResult<IEnumerable<MyAccountResponseModel>>> GetMyTransactionAccounts()
        {
            var result = await this.transactionService.GetMyAccounts();

            if(result.Any())
            {
                return Ok(result);
            }

            return NotFound(result);
        }

        [HttpPost]
        [Route("transaction-accounts/create")]
        public async Task<ActionResult<bool>> CreateTransactionAccount(TransactionAccRequestModel model)
        {

            var result = await this.transactionService.CreateAccount(model);

             if(result != true)
            {
                return Ok(result);
            }

            return Unauthorized(result);
        }

        [HttpPost]
        [Route("transaction-accounts/update")]
        public async Task<string> UpdateTranactionAccount(TransactionAccUpdateModel model)
            => await this.transactionService.UpdateAccount(model);

        [HttpPost]
        [Route("transaction-accounts/delete")]
        public async Task<ActionResult<string>> DeleteTransactionAccount(RequestByIdModel model)
        {
           if(!ModelState.IsValid)
            {
                return BadRequest(model?.Id.ToString());
            }

            return Ok(await this.transactionService.DeleteAccount(model));
        }
    }
}
