namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Services.Transaction;
    using Microsoft.AspNetCore.Mvc;

    public class TransactionController : ApiController
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddTransaction(MoneyTransactionRequestModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(false);
            }

            var result = await this.transactionService.AddTransaction(model);
            return Ok(result);
        }
    }
}
