using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Alpha.Common.Consul;

public static class ConsulServicesExtensions
{
    public static IServiceCollection ConsulServicesConfig(this IServiceCollection services, ConsulConfig consulConfig )
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
        {
            config.Address = new Uri(consulConfig.ConsulAddress!);
        }));

        services.AddSingleton<IHostedService, ConsulHostedService>();
        services.AddSingleton(consulConfig);
        return services;
    }
}