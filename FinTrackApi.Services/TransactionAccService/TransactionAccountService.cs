namespace FinTrackApi.Services
{
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Common.GlobalContants;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Infrastructure.Services;

    public class TransactionAccountService : ITransactionAccountService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;

        public TransactionAccountService(FinTrackApiDbContext dbContext, ICurrentUserService currentUserService)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
        }

        public async Task<bool> CreateAccount(TransactionAccRequestModel requestModel)
        {
            var userId = this.currentUserService.GetId();

            if(requestModel != null)
            {
                TransactionAccount transactionAcc = new()
                {
                    UserId = userId,
                    TransactionAccName = requestModel.AccountName,
                    TransactionAccType = requestModel.AccountType
                };

                await this.dbContext.AddAsync(transactionAcc);
                await this.dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<string> DeleteAccount(RequestByIdModel model)
        {
            if(model.Id != null)
            {

                var isDeleted = this.dbContext.TransactionAccounts
                    .Where(x => x.TransactionAccountId.Equals(model.Id))
                    .Select(x => x.IsDeleted)
                    .FirstOrDefault();

                if (isDeleted.Equals(false))
                {
                
                    isDeleted = true;
                    await this.dbContext.SaveChangesAsync();
                
                    return GlobalContants.Deleted;
                }
            }

            return GlobalContants.NotFound;
        }

        public async Task<string> UpdateAccount(TransactionAccUpdateModel requestModel)
        {
            if(requestModel != null)
            {
                var property = this.dbContext.TransactionAccounts
                    .Where(x => x.TransactionAccountId.Equals(requestModel.Id))
                    .Select(x => requestModel.Property)
                    .FirstOrDefault();

                if(property != null)
                {

                    property = requestModel.Value ?? property;

                    await this.dbContext.SaveChangesAsync();

                    return GlobalContants.Update;
                }
            }

           return GlobalContants.NotFound;
        }
    }
}
