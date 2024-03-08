using System.Net;
using Consul;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Alpha.Utils.Consul;

public class ConsulHostedService(IConsulClient consulClient, ConsulConfig consulConfig, 
    ILogger<ConsulHostedService> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        AgentServiceRegistration registration = CreateRegistration();

        logger.LogInformation($"Registering service with Consul: {registration.Name}");

        await consulClient.Agent.ServiceDeregister(registration.ID, cancellationToken);
        await consulClient.Agent.ServiceRegister(registration, cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deregistering service from Consul: {consulConfig.ServiceId}");

        AgentServiceRegistration registration = CreateRegistration();

        await consulClient.Agent.ServiceDeregister(registration.ID, CancellationToken.None);
    }

    private AgentServiceRegistration CreateRegistration()
    {
        var host = Dns.GetHostName();
        var stringPort = Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORTS");
        var port = stringPort is null ? 8080 : int.Parse(stringPort);
        var address = $"http://{host}:{port}";

        var registration = new AgentServiceRegistration
        {
            ID = address,
            Name = consulConfig.ServiceId,
            Address = host,
            Port = port
        };

        var check = new AgentServiceCheck
        {
            HTTP = $"{address}/health",
            Interval = TimeSpan.FromSeconds(20),
            Timeout = TimeSpan.FromSeconds(2)
        };

        registration.Checks = [check];
        return registration;
    }
}