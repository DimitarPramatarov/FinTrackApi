namespace FinTrackApi.Data.Models.Base
{
    public class Entitiy : IEntity
    {
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime? ModifedOn { get; set; } = null!;

    }
}
