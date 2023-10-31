using Artema.Platform.Application.Interfaces;
using Artema.Platform.Infrastructure.Common.Services;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;

namespace Artema.Platform.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services
            .AddSingleton<IUniqueIdentifierGenerator, PgUniqueIdentifierGenerator>()
            .AddSingleton<IClock>(SystemClock.Instance);

        return services;
    }
}