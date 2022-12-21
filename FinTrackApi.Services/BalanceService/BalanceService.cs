namespace FinTrackApi.Services.BalanceService
{
    using AutoMapper;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;
    using Microsoft.EntityFrameworkCore;

    public class BalanceService : IBalanceService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly IMapper mapper;

        public BalanceService(FinTrackApiDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteBalance(RequestByIdModel id)
        {
            if (id.Id != null)
            {

                var balance = await this.dbContext.Balances
                    .Where(x => x.BalanceId.Equals(id.Id))
                    .FirstOrDefaultAsync();

                balance.IsDeleted = true;

                await this.dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<BalanceResponseModel> GetAccountBalance(RequestByIdModel id)
        {
            if (id.Id == null)
            {
                return null!;
            }

            var result = await this.dbContext
                .Balances
                .FirstOrDefaultAsync(x => x.TransactionAccountId.Equals(id.Id));

            if (result != null)
            {
                var balance = this.mapper.Map<BalanceResponseModel>(result);

                return balance;
            }

            return null!;

        }

        public async Task<Balance> InitBalance(string id)
        {

            Balance balance = new()
            {
                TransactionAccountId = id,
                TotalBalance = 0,
                PreviousBalance = 0,
            };

            await this.dbContext.Balances.AddAsync(balance);
            await this.dbContext.SaveChangesAsync();

            return balance;
        }

        public async Task<bool> ResetBalance(RequestByIdModel id)
        {
            if (id?.Id != null)
            {
                var balance = await this.dbContext.Balances
                    .FirstOrDefaultAsync(x => x.BalanceId.Equals(id.Id));

                if (balance != null)
                {
                    balance.TotalBalance = 0.00M;
                    balance.PreviousBalance = 0.00M;

                    await this.dbContext.SaveChangesAsync();
                }

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateBalance(string id)
        {
            var lastTransaction = await this.dbContext.MoneyTransactions
                .Where(x => x.BalanceId.Equals(id))
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync();

            var currentBalance = await this.dbContext.Balances
                .FirstOrDefaultAsync(x => x.BalanceId.Equals(id));

            if (lastTransaction != null && currentBalance != null)
            {

                currentBalance.PreviousBalance = currentBalance.TotalBalance;
                var newBalance = CalculateTotalBalance(currentBalance.TotalBalance, lastTransaction.MoneyTransactionValue, lastTransaction.TransactionType.ToString());

                if (!newBalance.Equals(0.00M))
                {
                    currentBalance.PreviousBalance = currentBalance.TotalBalance;
                    currentBalance.TotalBalance = newBalance;

                    await this.dbContext.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        private decimal CalculateTotalBalance(decimal currentBalance, decimal transaction, string type)
        {
            if (type.Equals("Income"))
            {
                return currentBalance + transaction;
            }

            if (type.Equals("Outcome"))
            {
                return currentBalance - transaction;
            }

            return 0.00M;
        }
    }
}
