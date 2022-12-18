namespace FinTrackApi.Models.ResponseModels.TransactionAccResposeModels
{
    public class MyAccountResponseModel : BaseEntityResponseModel, IMyAccountReponseModel
    {
        public required string MyTransactionAccId { get; set; }

        public required string MyTransactionAccName { get; set; }

        public required string MyTransactionAccType{ get; set; }
    }
}
