namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TransactionAccountController
    {
        private readonly ITransactionAccountService transactionService;

        public TransactionAccountController(ITransactionAccountService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<ActionResult<bool>> CreateTransactionAccount(TransactionAccRequestModel model)
            => await this.transactionService.CreateAccount(model);

        [HttpPost]
        [Route("/update")]
        public async Task<string> UpdateTranactionAccount(TransactionAccUpdateModel model)
            => await this.transactionService.UpdateAccount(model);

        [HttpPost]
        [Route("/delete")]
        public async Task<string> DeleteTransactionAccount(RequestByIdModel model)
            => await this.transactionService.DeleteAccount(model);
    }
}
