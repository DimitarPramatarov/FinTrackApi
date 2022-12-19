namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;

    public class TransactionAccProfile : Profile
    {
        public TransactionAccProfile()
        {
            CreateMap<TransactionAccRequestModel, TransactionAccount>();
        }
    }
}
