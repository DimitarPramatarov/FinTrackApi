namespace FinTrackApi.Infrastructure.AutoMapperProfiles
{
    using AutoMapper;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Models.ResponseModels.TransactionResponseModels;

    public class TransactionProfile : Profile
    {
        public TransactionProfile() 
        {
            CreateMap<MoneyTransactionRequestModel, MoneyTransaction>();
            CreateMap<MoneyTransaction, TransactionResponseModel>();
        }
    }
}
