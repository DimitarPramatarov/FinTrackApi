namespace FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs
{
    public interface IBalanceResponseModel : IBaseEntityResponseModel
    {
        public string BalanceId { get; set; }

        public string TotalBalance { get; set; }

        public string PreviousBalance { get; set; }
    }
}