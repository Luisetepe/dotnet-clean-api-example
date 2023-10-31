using Artema.Platform.Infrastructure.Data.DbContexts;

namespace Artema.Platform.Infrastructure.Data.Seeding;

public interface IDatabaseSeeder
{
    Task SeedTestDataAsync(ArtemaPlatformDbContext dbContext);
}