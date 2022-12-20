namespace FinTrackApi.Models.ResponseModels.TransactionAccResposeModels
{
    public interface IMyAccountReponseModel
    {
        string TransactionAccountId { get; set; }

        string TransactionAccName { get; set; }

        string TransactionAccType { get; set; }
    }
}
