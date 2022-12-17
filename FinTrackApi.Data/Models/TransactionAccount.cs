namespace FinTrackApi.Data.Models
{
    using FinTrackApi.Data.Models.Base;
    using FinTrackApi.Data.Models.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class TransactionAccount : DeletableEntity , ITransactionAccount
    {
        [Key]
        public string TransactionAccountId { get; set; } = Guid.NewGuid().ToString();

        public required string TransactionAccName { get; set; } 
        
        public required string TransactionAccType { get; set; }

        public virtual User User { get; set; }

        public string UserId { get; set; }
    }
}
