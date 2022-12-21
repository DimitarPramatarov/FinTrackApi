namespace FinTrackApi.Services.BalanceService
{
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;

    public interface IBalanceService
    {
        Task<Balance> InitBalance(string id);

        Task<bool> UpdateBalance(string id);

        Task<bool> ResetBalance(RequestByIdModel id);

        Task<bool> DeleteBalance(RequestByIdModel id);

        Task<BalanceResponseModel> GetAccountBalance(RequestByIdModel id);
    }
}
