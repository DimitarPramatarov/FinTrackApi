namespace FinTrackApi.Services.Transaction
{
    using AutoMapper;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Services.BalanceService;

    public class TransactionService : ITransactionService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IBalanceService balanceService;

        public TransactionService(FinTrackApiDbContext dbContext,
            IBalanceService balanceService,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.balanceService = balanceService;
        }

        public async Task<bool> AddTransaction(MoneyTransactionRequestModel model)
        {
            if(model != null)
            {
                var transaction = this.mapper.Map<MoneyTransaction>(model);

                await this.dbContext.MoneyTransactions.AddAsync(transaction);
                await this.dbContext.SaveChangesAsync();
                await this.balanceService.UpdateBalance(transaction.BalanceId);

                return true;
            }

            return false;
        }
    }
}
