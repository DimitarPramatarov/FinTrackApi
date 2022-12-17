using System.ComponentModel.DataAnnotations;

namespace FinTrackApi.Models.RequestModels.TransactionAccModels
{
    public sealed class TransactionAccRequestModel
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string AccountName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(20)]
        public string AccountType { get; set; } = string.Empty;
    }
}
