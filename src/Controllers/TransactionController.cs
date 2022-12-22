namespace FinTrackApi.Controllers
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Models.ResponseModels.TransactionResponseModels;
    using FinTrackApi.Services.Transaction;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class TransactionController : ApiController
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this.transactionService = transactionService;
        }

        [HttpPost]
        [Route("/transactions/add")]
        public async Task<ActionResult<bool>> AddTransaction(MoneyTransactionRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(false);
            }

            var result = await this.transactionService.AddTransaction(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("/transactions/delete")]
        public async Task<ActionResult<bool>> DeleteTransaction(RequestByIdModel requestById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(false);
            }

            var result = await this.transactionService.DeleteTransaction(requestById);

            return Ok(result);
        }

        [HttpPost]
        [Route("/transactions/get-transaction")]
        public async Task<ActionResult<TransactionResponseModel>> GetTransaction(RequestByIdModel requestById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestById);
            }

            var transaction = await this.transactionService.GetTransaction(requestById);
            return Ok(transaction);
        }

        [HttpPost]
        [Route("/transactions/all")]
        public async Task<ActionResult<IEnumerable<TransactionResponseModel>>> GetAllTransaction(RequestByIdModel requestById)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestById);
            }

            var transactions = await this.transactionService.GetAllTransactions(requestById);

            return Ok(transactions);
        }
    }
}