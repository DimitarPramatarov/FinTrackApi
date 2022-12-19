namespace FinTrackApi.Services
{
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Common.GlobalContants;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Infrastructure.Services;
    using Microsoft.EntityFrameworkCore;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;
    using FinTrackApi.Services.BalanceService;
    using AutoMapper;

    public class TransactionAccountService : ITransactionAccountService
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IBalanceService balanceService;
        private readonly IMapper mapper;

        public TransactionAccountService(FinTrackApiDbContext dbContext, ICurrentUserService currentUserService,
            IBalanceService balanceService, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.currentUserService = currentUserService;
            this.balanceService = balanceService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateAccount(TransactionAccRequestModel requestModel)
        {
            var userId = this.currentUserService.GetId();

            if(requestModel != null)
            {
                var transactionAcc = this.mapper.Map<TransactionAccount>(requestModel);
                transactionAcc.UserId = userId;

                await this.dbContext.TransactionAccounts.AddAsync(transactionAcc);
                await this.dbContext.SaveChangesAsync();
                await this.balanceService.InitBalance(transactionAcc.TransactionAccountId);
                
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

        public async Task<IEnumerable<MyAccountResponseModel>> GetMyAccounts()
        {
            var currentUser =  this.currentUserService.GetId();

                var accounts = await this.dbContext.TransactionAccounts
                    .Where(x => x.UserId.Equals(currentUser))
                    .Select(x => new MyAccountResponseModel
                    { 
                         CreatedOn = x.CreatedOn,
                         MyTransactionAccId = x.TransactionAccountId,
                         MyTransactionAccName = x.TransactionAccName,
                         MyTransactionAccType = x.TransactionAccType
                    })
                    .ToListAsync<MyAccountResponseModel>();

            return accounts;
        }

        public async Task<string> UpdateAccount(TransactionAccUpdateModel requestModel)
        {
            if(requestModel != null)
            {
                var property = await this.dbContext.TransactionAccounts
                    .Where(x => x.TransactionAccountId.Equals(requestModel.Id))
                    .Select(x => requestModel.Property)
                    .FirstOrDefaultAsync();

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
