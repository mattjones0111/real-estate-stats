using System.Net.Http;
using System.Threading.Tasks;
using FundaApiClient;
using NUnit.Framework;
using Tests.Doubles;

namespace Tests
{
    [TestFixture]
    public class RetrialTests
    {
        [Test]
        public async Task GivenHttpUnauthorizedException_ApiClientRetries()
        {
            // arrange
            FundaApiClient.Settings.ApiClientSettings.Instance.ApiKey = "1234";

            StubHttpClientHandler httpClientHandlerStub =
                new StubHttpClientHandler();

            HttpClientHandler HandlerFactory() => httpClientHandlerStub;

            FundaDocumentProvider sut =
                new FundaDocumentProvider(HandlerFactory);

            // act
            await sut.GetPropertiesAsync("/amsterdam");

            // assert
            Assert.IsTrue(httpClientHandlerStub.MagicPageWasRetried);
        }
    }
}
