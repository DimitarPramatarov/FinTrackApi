using FinTrackApi.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinTrackApi.Data
{
    public class FinTrackApiDbContext : IdentityDbContext<User>
    {
        public FinTrackApiDbContext(DbContextOptions<FinTrackApiDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}