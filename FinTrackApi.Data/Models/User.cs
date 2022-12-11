using FinTrackApi.Data.Models.Base;
using Microsoft.AspNetCore.Identity;

namespace FinTrackApi.Data.Models
{
    public class User : IdentityUser, IEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = "User";
        public DateTime? ModifedOn { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = "User";
    }
}

