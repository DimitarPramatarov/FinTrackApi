using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;

namespace FinTrackApi.Models.ResponseModels.TransactionAccResposeModels
{
    public sealed class TransactionAccResponseModel : BaseEntityResponseModel, ITransactionAccResponseModel
    {
        public required string TransactionAccountId { get; set; }

        public required string TransactionAccType { get; set; }

        public required string TransactionAccName { get; set; }

        public required BalanceResponseModel BalanceResponse { get; set; }
    }
}
