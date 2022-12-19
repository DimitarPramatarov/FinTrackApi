namespace FinTrackApi.Data.Models
{
    using FinTrackApi.Data.Models.Base;
    using FinTrackApi.Data.Models.Enums;
    using FinTrackApi.Data.Models.Interfaces;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class MoneyTransaction : DeletableEntity, IMoneyTransaction
    {
        [Key]
        public string MoneyTransactionId { get; set; } = Guid.NewGuid().ToString();

        public required TransactionType TransactionType { get; set; } = TransactionType.Income;

        [Column(TypeName = "decimal(18,2)")]
        public required decimal MoneyTransactionValue { get; set; }

        public required string MoneyTransactionName { get; set; }

        public Balance? Balance { get; set; }

        public string? BalanceId { get; set; }
    }
}
