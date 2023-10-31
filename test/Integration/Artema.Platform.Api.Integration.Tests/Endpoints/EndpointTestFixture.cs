using Artema.Platform.Infrastructure.Data;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Artema.Platform.Api.Integration.Tests.Endpoints;

public class EndpointTestFixture : TestFixture<Program>
{
    public EndpointTestFixture(IMessageSink s) : base(s)
    {
    }

    protected override async Task SetupAsync()
    {
        var dbContext = Services.GetRequiredService<ArtemaPlatformDbContext>();
        
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var seeder = new EfCoreSeeder(dbContext);
        await seeder.SeedTestDataAsync(ArtemaPlatformInfrastructureDataAssembly.Reference);
    }
    
    protected override void ConfigureServices(IServiceCollection services)
    {
        // Uncomment below for using InMemory EFCore instead of real database driver.
        
        // services
        //     .RemoveAll(typeof(DbContext))
        //     .RemoveAll(typeof(DbContextOptions))
        //     .RemoveAll(typeof(ArtemaPlatformDbContext));
        //     .AddDbContext<ArtemaPlatformDbContext>(conf => conf.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Singleton);
    }

    protected override async Task TearDownAsync()
    {
        var dbContext = Services.GetRequiredService<ArtemaPlatformDbContext>();

        await dbContext.Database.CloseConnectionAsync();
        await dbContext.Database.EnsureDeletedAsync();
    }
    
    public ArtemaPlatformDbContext GetDbContext()
    {
        return Services.GetRequiredService<ArtemaPlatformDbContext>();
    }
}