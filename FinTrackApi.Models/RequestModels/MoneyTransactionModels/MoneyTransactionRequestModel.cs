namespace FinTrackApi.Models.RequestModels.MoneyTransactionModels
{
    using FinTrackApi.Models.RequestModels.RequestEnums;
    using System.ComponentModel.DataAnnotations;

    public class MoneyTransactionRequestModel
    {
        [Required]
        public  required string BalanceId { get; set; }

        [Required]
        public required TransactionRequestEnum TransactionType { get; set; }

        [Required]
        public required decimal MoneyTransactionValue { get; set; }

        [Required]
        public required string MoneyTransactionName {get; set;}
      
    }
}
