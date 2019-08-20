using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FundaApiClient.Exceptions;
using FundaApiClient.Model;
using FundaApiClient.Settings;
using Newtonsoft.Json;
using NLog;
using Polly;
using Polly.Retry;

namespace FundaApiClient
{
    public sealed class FundaDocumentProvider : IFetchProperties
    {
        readonly Func<HttpClientHandler> _handlerFactory;

        static readonly ILogger Log =
            LogManager.GetLogger(nameof(FundaDocumentProvider));

        static readonly AsyncRetryPolicy<HttpResponseMessage> RetryPolicy =
            Policy<HttpResponseMessage>
                .Handle<RateLimitExceededException>()
                .WaitAndRetryAsync(
                    ApiClientSettings.Instance.RetryCounts,
                    retryAttempt =>
                    {
                        Log.Debug($"Rate limit hit, retrying, attempt={retryAttempt}.");

                        return TimeSpan.FromSeconds(Math.Pow(retryAttempt, 2));
                    });

        public FundaDocumentProvider(
            Func<HttpClientHandler> handlerFactory = null)
        {
            _handlerFactory = handlerFactory ?? (() => new HttpClientHandler());
        }

        public async Task<IEnumerable<Property>> GetPropertiesAsync(
            string searchText)
        {
            List<Property> result = new List<Property>();

            int pageNumber = 1;

            using (HttpClient client = new HttpClient(_handlerFactory()))
            {
                do
                {
                    Uri url = BuildUrl(
                        ApiClientSettings.Instance.ApiKey,
                        searchText,
                        pageNumber);

                    HttpResponseMessage response = await RetryPolicy
                        .ExecuteAsync(
                            async () => await GetResponse(client, url));

                    string json = await response.Content.ReadAsStringAsync();

                    Document document = JsonConvert.DeserializeObject<Document>(json);

                    if (!document.Objects.Any())
                    {
                        break;
                    }

                    int count = document.Objects.Length;

                    Log.Debug($"Retrieved {count} object(s) from {url}.");

                    result.AddRange(document.Objects);

                    pageNumber++;

                } while (true);
            }

            return result;
        }

        static async Task<HttpResponseMessage> GetResponse(
            HttpClient client,
            Uri url)
        {
            Log.Debug($"About to GET from '{url}'.");

            HttpResponseMessage requestResult = await client.GetAsync(url);

            Log.Debug($"Request to {url} " +
                      $"returned HTTP status {requestResult.StatusCode:D} " +
                      $"{requestResult.StatusCode}.");

            if (requestResult.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new RateLimitExceededException();
            }

            requestResult.EnsureSuccessStatusCode();

            return requestResult;
        }

        static Uri BuildUrl(
            string key,
            string searchText,
            int pageNumber = 1,
            string type = "koop")
        {
            var url = string.Concat(
                $"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{key}/",
                $"?type={type}",
                $"&zo={searchText}",
                $"&page={pageNumber}",
                $"&pagesize={ApiClientSettings.Instance.DefaultPageSize}"
            );

            return new Uri(url);
        }
    }
}
