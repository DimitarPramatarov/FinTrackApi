using AutoMapper;
using FakeItEasy;
using FinTrackApi.Data;
using FinTrackApi.Data.Models;
using FinTrackApi.Data.Models.Enums;
using FinTrackApi.Infrastructure.AutoMapperProfiles;
using FinTrackApi.Infrastructure.Services;
using FinTrackApi.Models.RequestModels.CommonRequestModels;
using FinTrackApi.Services.BalanceService;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinTrackApi.Tests
{
    public class BalanceServiceTests 
    {
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;

        public BalanceServiceTests()
        {
            this.currentUserService = A.Fake<ICurrentUserService>();

            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new BalanceProfile());
                });
                mapper = mappingConfig.CreateMapper();
            }
        }
        public FinTrackApiDbContext AddMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinTrackApiDbContext>();
            optionsBuilder.UseInMemoryDatabase("FinTrackApiDb");
            var dbContext = new FinTrackApiDbContext(optionsBuilder.Options, this.currentUserService);
            return dbContext;
        }


        [Fact]
        public async Task InitBalance_ShouldReturnBalances()
        {
            //arr
            var dbContext = AddMemory();


            var service = new BalanceService(dbContext, mapper);

            var transactionAccountId = "1";
            //act
            var result = await service.InitBalance(transactionAccountId);

            //asser
            result.GetType().Should().Be(typeof(Balance));
            var isAdded = await dbContext.Balances
                .FirstOrDefaultAsync(x => x.TransactionAccountId.Equals(transactionAccountId));

            isAdded.Should().NotBeNull();
            isAdded.TransactionAccountId.Should().Be(transactionAccountId);
        }

        [Fact]
        public async Task ResetBalance_ShouldBeReset()
        {
            var dbContext = AddMemory();

            var balance = new Balance()
            {
                TransactionAccountId = "1",
                TotalBalance = 525.00M,
                PreviousBalance = 122.44M,
                BalanceId = "1",
            };

            await dbContext.AddAsync(balance);
            await dbContext.SaveChangesAsync();

            var service = new BalanceService(dbContext, mapper);

            var requestById = new RequestByIdModel()
            { Id = "1"};


            //act
            var result = await service.ResetBalance(requestById);

            //assert
            result.Should().Be(true);
            
            var updatedBalance = await dbContext.Balances
                .FirstOrDefaultAsync(x => x.BalanceId.Equals(requestById.Id));
           
            updatedBalance.TotalBalance.Should().Be(0.00M);
            updatedBalance.PreviousBalance.Should().Be(0.00M);
        }

        [Fact]
        public async Task UpdateBalance_ShouldBeUpdated()
        {
            //arr
            var dbContext = AddMemory();

            var balance = new Balance()
            {
                TransactionAccountId = "1",
                TotalBalance = 525.00M,
                PreviousBalance = 122.44M,
                BalanceId = "1",
            };

            var transaction = new MoneyTransaction()
            {
                BalanceId = "1",
                MoneyTransactionValue = 100.00M,
                MoneyTransactionId = "1",
                TransactionType = 0,
                 MoneyTransactionName = "Food",
            };

            await dbContext.Balances.AddAsync(balance);
            await dbContext.MoneyTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();

            var service = new BalanceService(dbContext, mapper);

            //act
            var result =  await service.UpdateBalance("1");

            //assert
            result.Should().Be(true);
            var updatedBlanace = await dbContext.Balances
                .FirstOrDefaultAsync(x => x.BalanceId.Equals("1"));

            updatedBlanace.TotalBalance.Should().Be(625.00M);
            updatedBlanace.PreviousBalance.Should().Be(525.00M);
        }

        [Fact]
        public async Task DeleteBalance_ShouldDeleteBalance()
        {
            var dbContext = AddMemory();

            var balance = new Balance()
            {
                TransactionAccountId = "1",
                TotalBalance = 525.00M,
                PreviousBalance = 122.44M,
                BalanceId = "1",
            };

            await dbContext.Balances.AddAsync(balance);
            await dbContext.SaveChangesAsync();

            var requestById = new RequestByIdModel()
            { Id = "1" };

            var service = new BalanceService(dbContext, mapper);

            //act
            var result = await service.DeleteBalance(requestById);

            //assert
            result.Should().Be(true);

            var isDeleted = await dbContext.Balances
                .FirstOrDefaultAsync(x => x.BalanceId.Equals(requestById.Id));

            isDeleted.IsDeleted.Should().Be(true);

        }

        [Fact]
        public async Task GetAccountBalance_ShouldReturnBalance()
        {
            var dbContext = AddMemory();

            var balance = new Balance()
            {
                TransactionAccountId = "1",
                TotalBalance = 525.00M,
                PreviousBalance = 122.44M,
                BalanceId = "1",
            };

            await dbContext.Balances.AddAsync(balance);
            await dbContext.SaveChangesAsync();

            var requestById = new RequestByIdModel()
            { Id = "1" };

            var service = new BalanceService(dbContext, mapper);

            //act
            var result = await service.GetAccountBalance(requestById);

            //asert
            result.Should().NotBeNull();
            result.BalanceId.Should().Be("1");
        }
    }
}
