namespace FinTrackApi.Services.Transaction
{
    using AutoMapper;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;

    public class TransactionService : ITransactionService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly IMapper mapper;

        public TransactionService(FinTrackApiDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> AddTransaction(MoneyTransactionRequestModel model)
        {
            if(model != null)
            {
                var transaction = this.mapper.Map<MoneyTransaction>(model);

                await this.dbContext.MoneyTransactions.AddAsync(transaction);
                await this.dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
