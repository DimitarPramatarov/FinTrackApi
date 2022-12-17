using FinTrackApi.Data.Models;
using FinTrackApi.Data.Models.Base;
using FinTrackApi.Infrastructure.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrackApi.Data
{
    public class FinTrackApiDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUserService;

        public FinTrackApiDbContext(DbContextOptions<FinTrackApiDbContext> options, ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<TransactionAccount> TransactionAccounts { get; init; }


        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess,cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
             .HasMany(x => x.TransactionAccounts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        }
        private void ApplyAuditInformation()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var username = this.currentUserService.GetUserName();

                    if (entry.Entity is DeletableEntity deletableEntity)
                    {
                        if (entry.State == EntityState.Deleted)
                        {
                            deletableEntity.DeletedOn = DateTime.UtcNow;
                            deletableEntity.IsDeleted = true;

                            entry.State = EntityState.Modified;

                            return;
                        }
                    }

                    if (entry.Entity is IEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                        }

                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifedOn = DateTime.UtcNow;
                        }
                    }
                });

        }
    }
}