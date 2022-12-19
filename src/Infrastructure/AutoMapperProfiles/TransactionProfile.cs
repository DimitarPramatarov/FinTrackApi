namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;

    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionAccRequestModel, TransactionAccount>();
        }
    }
}
