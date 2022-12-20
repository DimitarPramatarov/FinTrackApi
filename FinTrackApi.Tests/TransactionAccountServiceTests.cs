namespace FinTrackApi.Tests
{
    using AutoMapper;
    using FakeItEasy;
    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Infrastructure.AutoMapperProfiles;
    using FinTrackApi.Infrastructure.Services;
    using FinTrackApi.Models.RequestModels.CommonRequestModels;
    using FinTrackApi.Models.RequestModels.TransactionAccModels;
    using FinTrackApi.Services;
    using FinTrackApi.Services.BalanceService;
    using FluentAssertions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    using Xunit;

    public class TransactionAccountServiceTests
    {

        private readonly ICurrentUserService currentUserService;
        private readonly IBalanceService balanceService;
        private readonly IMapper mapper;

        public TransactionAccountServiceTests()
        {
            //this.mapper = A.Fake<IMapper>();
            this.balanceService = A.Fake<IBalanceService>();
            this.currentUserService = A.Fake<ICurrentUserService>();

            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new TransactionAccProfile());
                });
                mapper = mappingConfig.CreateMapper();
            }
        }



        public FinTrackApiDbContext AddMemory()
        {
            A.CallTo(() => this.currentUserService.GetId()).Returns("admin");
            var optionsBuilder = new DbContextOptionsBuilder<FinTrackApiDbContext>();
            optionsBuilder.UseInMemoryDatabase("FinTrackApiDb");
            var dbContext = new FinTrackApiDbContext(optionsBuilder.Options, this.currentUserService);
            return dbContext;
        }


        [Fact]
        public async Task GetMyAccount_ShouldRetrunAllMyAccountWithoutDeleted()
        {
            var dbContext = AddMemory();
            await dbContext.Database.EnsureDeletedAsync();
            var counter = 0;
            while (counter < 3)
            {
                var transactionAccount = new TransactionAccount
                {
                    UserId = "admin",
                    TransactionAccName = "Test",
                    TransactionAccType = "Test",
                    TransactionAccountId = Guid.NewGuid().ToString(),
                    IsDeleted = counter == 2 ? true : false,
                };

                await dbContext.TransactionAccounts.AddAsync(transactionAccount);
                await dbContext.SaveChangesAsync();
                counter++;
            }


            var service = new TransactionAccountService(dbContext, this.currentUserService, this.balanceService, mapper);

            //Act
            var result = await service.GetMyAccounts();

            //assert
            result.Should().NotBeNull();
            result.Count().Should().Be(2);
        }

        [Fact]
        public async Task TransactionAccount_ShouldBeDeleted()
        {
            var dbContext = AddMemory();

            var id = Guid.NewGuid().ToString();
            var transactionAccount = new TransactionAccount
            {
                TransactionAccName = "Test",
                TransactionAccType = "Test",
                TransactionAccountId = id,
                IsDeleted = false,
            };

            await dbContext.TransactionAccounts.AddAsync(transactionAccount);
            await dbContext.SaveChangesAsync();
            //arr
            var requestById = new RequestByIdModel { Id = id };
            var service = new TransactionAccountService(dbContext, this.currentUserService, this.balanceService, this.mapper);

            //Act
            var result = await service.DeleteAccount(requestById);

            //assert
            result.Should().Be("Succsefully deleted");
            dbContext.Entry(transactionAccount).Reload();
            var updatedTransactionAccount = dbContext.TransactionAccounts.FirstOrDefault(x => x.TransactionAccountId.Equals(id));
            updatedTransactionAccount.IsDeleted.Should().Be(true);

        }


        [Fact]
        public async Task UpdateAccount_ShouldBeUpdated()
        {

            //arr
            var dbContext = AddMemory();

            var transactionAccount = new TransactionAccount()
            {
                TransactionAccName = "Test",
                TransactionAccountId = "1",
                TransactionAccType = "0"
            };


            await dbContext.TransactionAccounts.AddAsync(transactionAccount);
            await dbContext.SaveChangesAsync();

            var requestModel = new TransactionAccUpdateModel()
            {
                TransactionAccountId = "1",
                Property = "TransactionAccName",
                Value = "Updated"
            };

            var service = new TransactionAccountService(dbContext, this.currentUserService, this.balanceService, this.mapper);

            //act
            var result = await service.UpdateAccount(requestModel);

            //assert
            result.Should().Be("Succsefully updated");
            var updated = dbContext.TransactionAccounts
                .FirstOrDefault(x => x.TransactionAccountId.Equals(transactionAccount.TransactionAccountId));
            updated.TransactionAccName.Should().Be("Updated");

        }

        [Fact]
        public async Task TransactionAccount_ShouldBeCreated()
        {
            //arr
            var dbContext = AddMemory();
           
            var transacitonRequestModel = new TransactionAccRequestModel()
            {
                TransactionAccName = "AddedAcc",
                TransactionAccType = "0",
            };

            var id = "fake";
            var balance = A.Fake<Balance>();
            A.CallTo(() => this.balanceService.InitBalance(id)).Returns(balance);

            var service = new TransactionAccountService(dbContext, this.currentUserService, this.balanceService, mapper);

            //act
            var result = await service.CreateAccount(transacitonRequestModel);

            //Assert
            result.Should().Be(true);

            var transactionAccIsAdded = await dbContext.TransactionAccounts
                .Where(x => x.TransactionAccName.Equals("AddedAcc"))
                .Select(x => x.TransactionAccName)
                .FirstOrDefaultAsync();

            transactionAccIsAdded
                .Should()
                .NotBeNullOrEmpty();

            transacitonRequestModel
                .TransactionAccName
                .Should()
                .BeSameAs("AddedAcc");
        }
    }
}