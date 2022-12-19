namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;

    public class BalanceProfile : Profile
    {
        public BalanceProfile ()
        {
            CreateMap<Balance, BalanceResponseModel>();
        }
    }
}
