namespace FinTrackApi.Services
{
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;

    public interface ITransactionAccountService
    {
        Task<IEnumerable<MyAccountResponseModel>> GetMyAccounts();

        Task<bool> CreateAccount(TransactionAccRequestModel requestModel);

        Task<string> UpdateAccount(TransactionAccUpdateModel requestModel);

        Task<string> DeleteAccount(RequestByIdModel model);
    }
}
