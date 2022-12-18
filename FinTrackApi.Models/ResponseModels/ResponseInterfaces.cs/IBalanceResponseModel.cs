namespace FinTrackApi.Models.ResponseModels.ResponseInterfaces.cs
{
    public interface IBalanceResponseModel : IBaseEntityResponseModel
    {
        public string BalanceId { get; set; }

        public string TotalBalance { get; set; }

        public string PreviosBalance { get; set; }
    }
}
