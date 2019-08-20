using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Doubles
{
    /// <summary>
    ///    A stub implementation of HttpClientHandler which returns
    ///    stubbed json data. The handler responds with non-empty object
    ///    data for any page less than or equal to 100.
    ///    Requests for a 'Magic Page' will throw an unauthorized HTTP status
    ///    code a configured number of times.
    /// </summary>
    public class StubHttpClientHandler : HttpClientHandler
    {
        public const int MagicPageNumber = 66;
        public const int RetriesBeforeSuccess = 3;

        private int _magicPageRetries;

        public bool MagicPageWasRetried { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var queryParams = request.RequestUri
                .Query
                .TrimStart('?')
                .Split("&")
                .ToDictionary(x => x.Split("=")[0], y => y.Split("=")[1]);

            var pageNumber = int.Parse(queryParams["page"]);

            if (pageNumber == MagicPageNumber &
                _magicPageRetries < RetriesBeforeSuccess)
            {
                MagicPageWasRetried = true;
                _magicPageRetries++;

                return Task.FromResult(
                    new HttpResponseMessage(HttpStatusCode.Unauthorized));
            }

            var content = File.ReadAllText(
                pageNumber > 100 ?
                    "200responseEmpty.json" :
                    "200response.json");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(content)
            };

            return Task.FromResult(result);
        }
    }
}