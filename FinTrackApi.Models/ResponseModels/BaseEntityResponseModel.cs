namespace FinTrackApi.Models.ResponseModels
{
    public class BaseEntityResponseModel : IBaseEntityResponseModel
    {
        public required DateTime CreatedOn { get; set; }
    }
}
