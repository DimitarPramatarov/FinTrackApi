using FinTrackApi.Data.Models.Base;

namespace FinTrackApi.Data.Models.Interfaces
{
    public interface ITransactionAccount : IDeletableEntity
    {
        public string TransactionAccountId { get; set; }

        public string TransactionAccName { get; set; }

        public string TransactionAccType { get; set; }
    }
}
