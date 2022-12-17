
namespace FinTrackApi.Data.Models.Base
{
    public class DeletableEntity : Entitiy, IDeletableEntity
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime DeletedOn { get; set; } = DateTime.UtcNow;
    }
}
