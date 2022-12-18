namespace FinTrackApi.Data.Models
{
    using FinTrackApi.Data.Models.Base;
    using FinTrackApi.Data.Models.Interfaces;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Balance : DeletableEntity, IBalance
    {
        [Key]
        public string BalanceId { get; set; } = Guid.NewGuid().ToString();

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalBalance { get; set; } = 0.00M;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PreviousBalance { get; set; } = 0.00M;

        public virtual TransactionAccount? TransactionAccount { get; set; }

        public string? TransactionAccountId { get; set; }
    }
}
