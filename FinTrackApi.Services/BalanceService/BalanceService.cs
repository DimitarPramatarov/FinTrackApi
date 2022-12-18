namespace FinTrackApi.Services.BalanceService
{
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.ResponseModels.BalanceResponseModels.cs;
    using Microsoft.EntityFrameworkCore;

    public class BalanceService : IBalanceService
    {
        private readonly FinTrackApiDbContext dbContext;

        public BalanceService(FinTrackApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<bool> DeleteBalance()
        {
            throw new NotImplementedException();
        }

        public async Task<BalanceResponseModel> GetAccountBalance(RequestByIdModel id)
        {
            if(id.Id == null)
            {
                return null!;
            }

            var result = await this.dbContext
                .Balances
                .FirstOrDefaultAsync(x => x.TransactionAccountId.Equals(id.Id));

            if(result != null)
            {
                BalanceResponseModel balance = new()
                { 
                    BalanceId = result.BalanceId,
                     CreatedOn = result.CreatedOn,
                     TotalBalance = result.TotalBalance.ToString(),
                     PreviosBalance = result.PreviousBalance.ToString(),
                };

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

        public Task<bool> ResetBalance()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateBalance()
        {
            throw new NotImplementedException();
        }
    }
}
