namespace FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs
{
    public sealed class BalanceResponseModel : BaseEntityResponseModel, IBalanceResponseModel
    {
        public required string BalanceId { get; set; }
        
        public required string TotalBalance { get; set; }

        public required string PreviousBalance { get; set; }
    }
}
