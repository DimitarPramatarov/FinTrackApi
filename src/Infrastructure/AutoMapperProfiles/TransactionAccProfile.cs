namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;

    public class TransactionAccProfile : Profile
    {
        public TransactionAccProfile()
        {
            CreateMap<TransactionAccRequestModel, TransactionAccount>();

            CreateMap<TransactionAccount, MyAccountResponseModel>();
        }
    }
}
