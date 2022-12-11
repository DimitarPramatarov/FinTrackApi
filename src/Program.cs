
namespace FinTrackApi
{

    using FinTrackApi.Data;
    using FinTrackApi.Data.Models;
    using FinTrackApi.Data.Seeding;
    using FinTrackApi.Infrastructure;
    using FinTrackApi.Infrastructure.Extensions;
    using FinTrackApi.Infrastructure.Services;
    using FinTrackApi.Services;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System.Text;

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
           
            builder.Services.RegisterServices(builder.Configuration);

            var app = builder.Build();

            using (var serviceScope = app.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<FinTrackApiDbContext>();
                var roleSeed = new RoleSeeder();
                roleSeed.Seed(dbContext, serviceScope.ServiceProvider);
                dbContext.SaveChanges();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}