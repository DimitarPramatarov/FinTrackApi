namespace FinTrackApi.Services.BalanceService
{
    using AutoMapper;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;
    using Microsoft.EntityFrameworkCore;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

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
                .FirstOrDefaultAsync(x => x.TransactionAccountId.Equals(id.Id) && x.IsDeleted.Equals(false));

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
                    .FirstOrDefaultAsync(x => x.BalanceId.Equals(id.Id) && x.IsDeleted.Equals(false));

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

        public async Task<bool> UpdateBalance(string balanceId, string transactionId)
        {
            var transaction = await this.dbContext.MoneyTransactions
                .Where(x => x.BalanceId.Equals(balanceId) && x.MoneyTransactionId.Equals(transactionId))
                .FirstOrDefaultAsync();

            var currentBalance = await this.dbContext.Balances
                .FirstOrDefaultAsync(x => x.BalanceId.Equals(balanceId));

            if (transaction != null && currentBalance != null)
            {

                currentBalance.PreviousBalance = currentBalance.TotalBalance;
                var newBalance = CalculateTotalBalance(currentBalance.TotalBalance, transaction.MoneyTransactionValue, transaction.TransactionType.ToString());

                if (!newBalance.Equals(0.00M))
                {
                    currentBalance.PreviousBalance = currentBalance.TotalBalance;

                    if(transaction.TransactionType.ToString().Equals("Deleted"))
                    {
                        currentBalance.PreviousBalance = CalculatePreviousBalanceIfDeleted(currentBalance.PreviousBalance, transaction.MoneyTransactionValue);
                    }

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

            if(type.Equals("Deleted"))
            {
                return currentBalance - transaction;
            }

            return 0.00M;
        }

        private decimal CalculatePreviousBalanceIfDeleted(decimal previousBalance, decimal deletedTransaction)
        {
            return previousBalance - deletedTransaction;
        }
    }
}
