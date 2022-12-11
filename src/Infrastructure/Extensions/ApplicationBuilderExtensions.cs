namespace FinTrackApi.Infrastructure.Extensions
{
    using FinTrackApi.Data;
    using FinTrackApi.Data.Seeding;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FinTrackApiDbContext>();
               
                dbContext.Database.Migrate();
            }
        }

        public static void RoleSeeder(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<FinTrackApiDbContext>();
                var roleSeed = new RoleSeeder();
                roleSeed.Seed(dbContext, serviceScope.ServiceProvider);
                dbContext.SaveChanges();
            }
        }
    }
}