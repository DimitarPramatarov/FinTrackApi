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
        private  readonly IMapper mapper;

        public BalanceService(FinTrackApiDbContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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
