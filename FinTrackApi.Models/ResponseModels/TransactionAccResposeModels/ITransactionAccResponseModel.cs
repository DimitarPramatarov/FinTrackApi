namespace FinTrackApi.Models.ResponseModels.TransactionAccResposeModels
{
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;

    public interface ITransactionAccResponseModel : IBaseEntityResponseModel
    {
        public  string TransactionAccountId { get; set; }

        public  string TransactionAccType { get; set; }

        public  string TransactionAccName { get; set; }

        public  BalanceResponseModel BalanceResponse { get; set; }
    }
}
