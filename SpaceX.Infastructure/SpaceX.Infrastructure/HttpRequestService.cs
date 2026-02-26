using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly.Contrib.WaitAndRetry;
using Polly;
using System.Net;
using SpaceX.Infrastructure.Exceptions;
using System.Text.Json;
using SpaceX.Infrastructure.Configurations;

namespace SpaceX.Infrastructure
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly SpaceXConfiguration _spaceXConfiguration;

        private readonly IAsyncPolicy<HttpResponseMessage> _retryPolicy = Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(p => p.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 3));

        private readonly ILogger<HttpRequestService> _logger;

        private readonly ICacheService _cacheService;
        public HttpRequestService(
            IOptions<SpaceXConfiguration> spaceXConfiguration,
            ILogger<HttpRequestService> logger,
            ICacheService cacheService)
        {
            _logger = logger;
            _spaceXConfiguration = spaceXConfiguration.Value;
            _cacheService = cacheService;
        }
        public async Task<T> GetResponseInJson<T>(string requestUri)
        {
            var item = await _cacheService.GetItemFromCache<T>(requestUri);
            if (item != null) return item;

            HttpClient client = new HttpClient { BaseAddress = new Uri(_spaceXConfiguration.BaseUrl) };
            var httpResponse = await _retryPolicy.ExecuteAsync(() => client.GetAsync(requestUri));

            string response = await httpResponse.Content.ReadAsStringAsync();

            if (!httpResponse.IsSuccessStatusCode || httpResponse.StatusCode != HttpStatusCode.OK)
            {
                _logger.LogCritical(response);

                throw new IntegrationException(response);
            }
            _logger.LogInformation(response);

            var result = JsonSerializer.Deserialize<T>(response);
            await _cacheService.SaveItemToCache(requestUri, result);
            return result!;
        }
    }
}
