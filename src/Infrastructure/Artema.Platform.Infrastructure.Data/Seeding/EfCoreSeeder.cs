using System.Reflection;
using Artema.Platform.Infrastructure.Data.DbContexts;

namespace Artema.Platform.Infrastructure.Data.Seeding;

public class EfCoreSeeder
{
    private readonly ArtemaPlatformDbContext _dbContext;

    public EfCoreSeeder(ArtemaPlatformDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedTestDataAsync(Assembly seedsAssembly)
    {
        await _dbContext.Database.EnsureCreatedAsync();
        
        var seedTasks = seedsAssembly
            .GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IDatabaseSeeder)))
            .ToList()
            .Select( async t =>
            {
                var seeder = (IDatabaseSeeder) Activator.CreateInstance(t)!;
                await seeder.SeedTestDataAsync(_dbContext);
            });
        
        await Task.WhenAll(seedTasks);
        await _dbContext.SaveChangesAsync();
    }
}