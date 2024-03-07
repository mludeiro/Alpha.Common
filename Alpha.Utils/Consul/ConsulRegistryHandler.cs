using System.Net;
using Consul;
using Microsoft.Extensions.Caching.Memory;

namespace Alpha.Utils.Consul;

public class ConsulRegistryHandler(IConsulClient consulClient, IMemoryCache memCache, 
    ILogger<ConsulRegistryHandler> logger) : DelegatingHandler
{
    private static readonly Random random = new();

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var serviceName = request.RequestUri!.Host;
        var service = await GetRandomCatalogService(serviceName, cancellationToken);
        
        if(service is null)
        {
            var error = $"Cant find a healty instance of service {serviceName} on consul registry.";
            logger.LogError(error);
            return new HttpResponseMessage(HttpStatusCode.BadGateway)
            {
                Content = new StringContent(error),
                RequestMessage = request
            };
        }

        logger.LogInformation($"Colsul resolved service {serviceName} on address {service.ServiceAddress}");

        request.RequestUri = new Uri($"http://{service.ServiceAddress}:{service.ServicePort}{request.RequestUri.PathAndQuery}");
        
        return await base.SendAsync(request, cancellationToken);
    }

    private async Task<CatalogService?> GetRandomCatalogService(string serviceName, CancellationToken cancellationToken)
    {
        var memKey = $"ConsulRegistryHandler/{serviceName}";
        var catalog = memCache.Get<QueryResult<CatalogService[]>>(memKey);

        if( catalog is null )
        {
            logger.LogInformation($"Colsul catalog request for service {serviceName}");
            catalog = await consulClient.Catalog.Service(serviceName,cancellationToken);
            memCache.Set(memKey, catalog, DateTimeOffset.Now.AddSeconds(30));
        }

        if(catalog.Response.Length == 0) return null;

        var randomPos = random.Next() % catalog.Response.Length;

        return catalog.Response[randomPos];
    }
}