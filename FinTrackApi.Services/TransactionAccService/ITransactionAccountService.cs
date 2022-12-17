namespace FinTrackApi.Services
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;

    public interface ITransactionAccountService
    {
        Task<bool> CreateAccount(TransactionAccRequestModel requestModel);

        Task<string> UpdateAccount(TransactionAccUpdateModel requestModel);

        Task<string> DeleteAccount(RequestByIdModel model);
    }
}
