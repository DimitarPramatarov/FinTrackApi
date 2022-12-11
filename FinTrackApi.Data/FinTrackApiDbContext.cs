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

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        private void ApplyAuditInformation()
        {
            this.ChangeTracker
                .Entries()
                .ToList()
                .ForEach(entry =>
                {
                    var username = this.currentUserService.GetUserName();

                    //if (entry.Entity is IDeletableEntity deletableEntity)
                    //{
                    //    if (entry.State == EntityState.Deleted)
                    //    {
                    //        deletableEntity.DeletedOn = DateTime.UtcNow;
                    //        deletableEntity.DeletedBy = username;
                    //        deletableEntity.IsDeleted = true;

                    //        entry.State = EntityState.Modified;

                    //        return;
                    //    }
                    //}

                    if (entry.Entity is IEntity entity)
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedOn = DateTime.UtcNow;
                            entity.CreatedBy = username;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            entity.ModifedOn = DateTime.UtcNow;
                            entity.ModifiedBy = username;
                        }
                    }
                });

        }
    }
}