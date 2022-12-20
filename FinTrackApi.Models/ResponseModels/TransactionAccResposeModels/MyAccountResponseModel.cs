namespace FinTrackApi.Models.ResponseModels.TransactionAccResposeModels
{
    public class MyAccountResponseModel : BaseEntityResponseModel, IMyAccountReponseModel
    {
        public required string TransactionAccountId { get; set; }

        public required string TransactionAccName { get; set; }

        public required string TransactionAccType{ get; set; }
    }
}
