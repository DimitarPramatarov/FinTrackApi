namespace FinTrackApi.Services.BalanceService
{
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;

    public interface IBalanceService
    {
        Task<Balance> InitBalance(string id);

        Task<bool> UpdateBalance();

        Task<bool> ResetBalance();

        Task<bool> DeleteBalance();

        Task<BalanceResponseModel> GetAccountBalance(RequestByIdModel id);
    }
}
