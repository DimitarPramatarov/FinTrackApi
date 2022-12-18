using FinTrackApi.Data.Models.Base;

namespace FinTrackApi.Data.Models.Interfaces
{
    public interface IBalance : IDeletableEntity
    {
        public string BalanceId { get; set; }

        public decimal TotalBalance { get; set; }

        public decimal PreviousBalance { get; set; }
    }
}
