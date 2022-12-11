namespace FinTrackApi.Data.Models.Base
{
    public interface IEntity
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifedOn { get; set; }
        public string ModifiedBy { get; set; } 
    }
}
