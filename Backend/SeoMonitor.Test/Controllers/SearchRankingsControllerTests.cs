using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

namespace SeoMonitor.Test.Controllers
{
    public class SearchRankingsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        
        public SearchRankingsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Integration_GetSearchRankings_ReturnsOk()
        {
            var client = _factory.CreateClient();
         
            var url = "/SearchRankings?searchText=HelloWorld&website=www.imdb.com";
            var response = await client.GetAsync(url);

            Assert.Equal(200, (int)response.StatusCode);
        }

        [Theory]
        [InlineData("/SearchRankings")]
        [InlineData("/SearchRankings?searchText=HelloWorld")]
        [InlineData("/SearchRankings?website=www.imdb.com")]
        [InlineData("/SearchRankings?searchText=&website=")]
        public async Task Integration_GetSearchRankings_ReturnsBadRequest(string url)
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync(url);
            
            Assert.Equal(400, (int)response.StatusCode);
        }
    }
}