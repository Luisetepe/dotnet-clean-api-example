using Artema.Platform.Infrastructure.Data;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;

try
{
    var builder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", true, true);

    var configurationRoot = builder.Build();

    var connectionString = configurationRoot.GetConnectionString("LocalDatabase");

    if (connectionString is null)
    {
        throw new KeyNotFoundException("Could not read database connectionString");
    }

    var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
    dataSourceBuilder.UseNodaTime();

    var dataSource = dataSourceBuilder.Build();
    var options = new DbContextOptionsBuilder<ArtemaPlatformDbContext>()
        .UseNpgsql(dataSource, conf => conf.UseNodaTime());
    var dbContext = new ArtemaPlatformDbContext(options.Options);

    var seeder = new EfCoreSeeder(dbContext);
    await seeder.SeedTestDataAsync(ArtemaPlatformInfrastructureDataAssembly.Reference);

    Console.WriteLine("All data was successfully seeded!");
}
catch (Exception ex)
{
    Console.WriteLine($"An error ocurred while seeding. Message: {ex.Message}");
}