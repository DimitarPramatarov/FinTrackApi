using Microsoft.AspNetCore.Identity;

namespace FinTrackApi.Data.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifedOn { get; set; }
        public string ModifiedBy { get; set; } = null!;
    }
}
