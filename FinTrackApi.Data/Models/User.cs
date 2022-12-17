namespace FinTrackApi.Data.Models
{
    using FinTrackApi.Data.Models.Base;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser, IEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ModifedOn { get; set; } = DateTime.UtcNow;

        public virtual ICollection<TransactionAccount> TransactionAccounts { get; set; } = new HashSet<TransactionAccount>();
    }
}

