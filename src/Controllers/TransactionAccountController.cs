namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;
    using FinTrackApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TransactionAccountController : ApiController    
    {
        private readonly ITransactionAccountService transactionService;

        public TransactionAccountController(ITransactionAccountService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpGet]
        [Route("/get/transaction-accounts")]
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
        [Route("/create")]
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
        [Route("/update")]
        public async Task<string> UpdateTranactionAccount(TransactionAccUpdateModel model)
            => await this.transactionService.UpdateAccount(model);

        //[HttpPost]
        //[Route("/delete")]
        //public async Task<string> DeleteTransactionAccount(RequestByIdModel model)
        //    => Ok(await this.transactionService.DeleteAccount(model));
    }
}
