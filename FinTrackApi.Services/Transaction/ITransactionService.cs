namespace FinTrackApi.Services.Transaction
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Models.ResponseModels.TransactionResponseModels;

    public interface ITransactionService
    {
        Task<bool> AddTransaction(MoneyTransactionRequestModel model);

        Task<bool> DeleteTransaction(RequestByIdModel requestById);

        Task<TransactionResponseModel> GetTransaction(RequestByIdModel requestById);

        Task<IEnumerable<TransactionResponseModel>> GetAllTransactions(RequestByIdModel requestById);
    }
}
