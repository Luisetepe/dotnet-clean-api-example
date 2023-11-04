using Artema.Platform.Application.Interfaces;
using Artema.Platform.Infrastructure.Data.DbContexts;
using Artema.Platform.Infrastructure.Data.Services;
using Artema.Platform.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Artema.Platform.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDataInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
    }

    public static IServiceCollection AddDbContextInfrastructure(this IServiceCollection services, string connectionString)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
        dataSourceBuilder.UseNodaTime();

        var dataSource = dataSourceBuilder.Build();

        return services
            .AddSingleton<IUniqueIdentifierGenerator, PgUniqueIdentifierGenerator>()
            .AddDbContext<ArtemaPlatformDbContext>(options =>
                options.UseNpgsql(dataSource, settings => settings.UseNodaTime())
            );
    }
}
