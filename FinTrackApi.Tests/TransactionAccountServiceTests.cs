namespace FinTrackApi.Tests
{
    using AutoMapper;
    using FakeItEasy;
    using FinTrackApi.Data;
    using FinTrackApi.Infrastructure.Services;
    using FinTrackApi.Models.ResponseModels.TransactionAccResposeModels;
    using FinTrackApi.Services.BalanceService;

    public class TransactionAccountServiceTests
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly ICurrentUserService currentUserService;
        private readonly IBalanceService balanceService;
        private readonly IMapper mapper;

        public TransactionAccountServiceTests()
        {
            this.mapper = A.Fake<IMapper>();
            this.dbContext = A.Fake<FinTrackApiDbContext>();
            this.balanceService = A.Fake<IBalanceService>();
            this.currentUserService = A.Fake<ICurrentUserService>();
        }

        [Fact]
        public void GetMyAccount_ShouldRetrunAllMyAccount()
        {
            //Arrange
            var myAccounts = A.Fake<ICollection<MyAccountResponseModel>>();

            //Act


            //Assert
        }
    }
}