using System.ComponentModel.DataAnnotations;

namespace FinTrackApi.Models.RequestModels.TransactionAccModels
{
    public sealed class TransactionAccRequestModel
    {
        [MinLength(3)]
        [MaxLength(20)]
        public string TransactionAccName { get; set; } = string.Empty;

        [MinLength(3)]
        [MaxLength(20)]
        public string TransactionAccType { get; set; } = string.Empty;
    }
}
