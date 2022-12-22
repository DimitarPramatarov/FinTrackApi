using AutoMapper;
using FakeItEasy;
using FinTrackApi.Data;
using FinTrackApi.Data.Models;
using FinTrackApi.Data.Models.Interfaces;
using FinTrackApi.Infrastructure.AutoMapperProfiles;
using FinTrackApi.Infrastructure.Services;
using FinTrackApi.Models.RequestModels.CommonRequestModels;
using FinTrackApi.Models.RequestModels.MoneyTransactionModels;
using FinTrackApi.Models.ResponseModels.TransactionResponseModels;
using FinTrackApi.Services.BalanceService;
using FinTrackApi.Services.Transaction;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Transactions;
using Xunit.Sdk;

namespace FinTrackApi.Tests
{
    public class TransactionServiceTests
    {
        private readonly FinTrackApiDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IBalanceService balanceService;
        private readonly ICurrentUserService currentUserService;

        public TransactionServiceTests()
        {
            this.currentUserService = A.Fake<ICurrentUserService>();
            this.balanceService = A.Fake<IBalanceService>();

            if (mapper == null)
            {
                var mappingConfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new TransactionProfile());
                });
                mapper = mappingConfig.CreateMapper();
            }
        }
        public FinTrackApiDbContext AddMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinTrackApiDbContext>();
            optionsBuilder.UseInMemoryDatabase("FinTrackApiDb");
            var dbContext = new FinTrackApiDbContext(optionsBuilder.Options, this.currentUserService);
            dbContext.Database.EnsureDeleted();
            return dbContext;
        }

        [Fact]
        public async Task AddTransaction_ShouldAddTransaction()
        {
            //arr
            var dbContext = AddMemory();

            var model = new MoneyTransactionRequestModel()
            {
                BalanceId = "1",
                MoneyTransactionName = "test",
                MoneyTransactionValue = 200.00M,
                TransactionType = 0
            };

            var service = new TransactionService(dbContext, this.balanceService, mapper);

            //act
            var result = await service.AddTransaction(model);

            //assert
            result.Should().Be(true);
            var transaction = await dbContext.MoneyTransactions
                .FirstOrDefaultAsync(x => x.BalanceId.Equals("1"));

            transaction.Should().NotBeNull();
        }

        [Fact]
        public async Task DeleteTransaction_ShouldBeDeleted()
        {
            //arr
            var dbContext = AddMemory();

            var requestById = new RequestByIdModel()
            { Id = "1" };

            var transaction = new MoneyTransaction()
            {
                BalanceId = "1",
                MoneyTransactionName = "test",
                MoneyTransactionValue = 200.00M,
                MoneyTransactionId = "1",
                TransactionType = 0
            };

            var balance = new Balance()
            {
                BalanceId = "1",
                TotalBalance = 500.00M,
                PreviousBalance = 100.00M,
                TransactionAccountId = "1"
            };

            await dbContext.Balances.AddAsync(balance);
            await dbContext.MoneyTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();

            var service = new TransactionService(dbContext, this.balanceService, mapper);

            //act
            var result = await service.DeleteTransaction(requestById);

            //asser
            result.Should().BeTrue();
            var currentTransaction = await dbContext.MoneyTransactions.FirstOrDefaultAsync(x => x.BalanceId.Equals(requestById.Id));
            currentTransaction.IsDeleted.Should().BeTrue();
        }

        [Fact]
        public async Task GetTransaction_ShouldReturnTransaction()
        {
            //arr
            var dbContext = AddMemory();

            var requestById = new RequestByIdModel()
            { Id = "1" };

            var transaction = new MoneyTransaction()
            {
                BalanceId = "1",
                MoneyTransactionName = "test",
                MoneyTransactionValue = 200.00M,
                MoneyTransactionId = "1",
                TransactionType = 0
            };

            await dbContext.MoneyTransactions.AddAsync(transaction);
            await dbContext.SaveChangesAsync();

            var service = new TransactionService(dbContext, this.balanceService, mapper);

            //act
            var result = await service.GetTransaction(requestById);

            //asser
            result.Should().NotBeNull();

            result.GetType().Should().Be(typeof(TransactionResponseModel));

            result.BalanceId.Should().Be("1");
        }

        [Fact]
        public async Task GetAllTransactions_ShouldReturnAll()
        {
            var dbContext = AddMemory();

            var requestById = new RequestByIdModel()
            { Id = "1" };

            var counter = 1;
            while(counter <= 3)
            {

                var transaction = new MoneyTransaction()
                {
                    BalanceId = "1",
                    MoneyTransactionName = "test",
                    MoneyTransactionValue = 200.00M,
                    MoneyTransactionId = counter.ToString(),
                    TransactionType = 0
                };
                counter++;
                await dbContext.MoneyTransactions.AddAsync(transaction);
                await dbContext.SaveChangesAsync();
            }

            var service = new TransactionService(dbContext, this.balanceService, mapper);

            //act
            var result = await service.GetAllTransactions(requestById);

            //assert
            result.Should().AllBeOfType(typeof(TransactionResponseModel));
            result.Count().Should().Be(3);
        }
    }
}
