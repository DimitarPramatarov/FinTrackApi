namespace FinTrackApi.Services.Transaction
{
    using AutoMapper;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Data.Models.Enums;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
    using FinTrackApi.Models.ResponseModels.TransactionResponseModels;
    using FinTrackApi.Services.BalanceService;
    using Microsoft.EntityFrameworkCore;

    public class TransactionService : ITransactionService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IBalanceService balanceService;

        public TransactionService(FinTrackApiDbContext dbContext,
            IBalanceService balanceService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.balanceService = balanceService;
        }

        public async Task<bool> AddTransaction(MoneyTransactionRequestModel model)
        {
            if (!string.IsNullOrEmpty(model.BalanceId))
            {
                var transaction = this.mapper.Map<MoneyTransaction>(model);

                await this.dbContext.MoneyTransactions.AddAsync(transaction);
                await this.dbContext.SaveChangesAsync();
                await this.balanceService.UpdateBalance(transaction.BalanceId, transaction.MoneyTransactionId);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteTransaction(RequestByIdModel requestById)
        {
            var transaction = await this.dbContext.MoneyTransactions
                .FirstOrDefaultAsync(x => x.MoneyTransactionId.Equals(requestById.Id));

            if (transaction != null)
            {
                transaction.IsDeleted = true;
                transaction.TransactionType = (TransactionType)1;
                
                if(transaction.BalanceId != null)
                {
                    await this.dbContext.SaveChangesAsync();
                    await this.balanceService.UpdateBalance(transaction.BalanceId, transaction.MoneyTransactionId);
                    return true;
                }
            }

            return false;
        }

        public async Task<TransactionResponseModel> GetTransaction(RequestByIdModel requestById)
        {
            var requestedTransaction = await this.dbContext.MoneyTransactions
                .FirstOrDefaultAsync(x => x.MoneyTransactionId.Equals(requestById.Id));

            if(requestedTransaction != null)
            {
                var transaction = mapper.Map<TransactionResponseModel>(requestedTransaction);
                return transaction;
            }

            return null!;
        } 

        public async Task<IEnumerable<TransactionResponseModel>> GetAllTransactions(RequestByIdModel requestById)
        {
            var allTransaction = await this.dbContext.MoneyTransactions
                .Where(x => x.BalanceId.Equals(requestById.Id) && x.IsDeleted.Equals(false))
                .Select(x => x)
                .ToListAsync();

           return mapper.Map<IEnumerable<TransactionResponseModel>>(allTransaction);
        }
    }
}
