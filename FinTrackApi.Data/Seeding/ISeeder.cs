namespace FinTrackApi.Data.Seeding
{
    public interface ISeeder
        {
            void Seed(FinTrackApiDbContext dbContext, IServiceProvider serviceProvider);
        }
}
