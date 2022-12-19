namespace FinTrackApi.Services.Transaction
{
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    
    public interface ITransactionService
    {
        Task<bool> AddTransaction(MoneyTransactionRequestModel model);
    }
}
