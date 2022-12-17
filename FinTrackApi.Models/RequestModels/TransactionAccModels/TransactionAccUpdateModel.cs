namespace FinTrackApi.Models.RequestModels.TransactionAccModels
{
    public sealed class TransactionAccUpdateModel
    {
        public required string Id { get; set; }

        public required string Property { get; set; }

        public required string Value { get; set; }
    }
}
