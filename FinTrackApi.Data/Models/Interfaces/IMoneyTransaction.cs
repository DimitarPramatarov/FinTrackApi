namespace FinTrackApi.Data.Models.Interfaces
{
    using FinTrackApi.Data.Models.Enums;

    public interface IMoneyTransaction
    {
        string MoneyTransactionId { get; set; } 

        TransactionType TransactionType { get; set; }

        decimal MoneyTransactionValue { get; set; }

        string MoneyTransactionName { get; set; }
    }
}
