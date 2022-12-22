namespace FinTrackApi.Models.ResponseModels.TransactionResponseModels
{
    public class TransactionResponseModel : BaseEntityResponseModel
    {
        public required string MoneyTransactionId { get; set; } 

        public required string TransactionType { get; set; } 

        public required string MoneyTransactionValue { get; set; }

        public required string MoneyTransactionName { get; set; }

        public required string BalanceId { get; set; }
    }
}
