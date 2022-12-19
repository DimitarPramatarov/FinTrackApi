using System.ComponentModel.DataAnnotations;

namespace FinTrackApi.Models.RequestModels.TransactionAccModels
{
    public sealed class TransactionAccRequestModel
    {
        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string TransactionAccName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string TransactionAccType { get; set; } = string.Empty;
    }
}
