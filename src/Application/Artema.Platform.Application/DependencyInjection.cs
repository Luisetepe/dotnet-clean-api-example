using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Artema.Platform.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        return services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}