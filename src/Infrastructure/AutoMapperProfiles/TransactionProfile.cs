namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;

    public class TransactionProfile : Profile
    {
        public TransactionProfile() 
        {
            CreateMap<MoneyTransactionRequestModel, MoneyTransaction>();
        }
    }
}
