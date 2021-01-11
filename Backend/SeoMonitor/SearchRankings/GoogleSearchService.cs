using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using MediatR;

namespace SeoMonitor.SearchRankings
{
    public interface IGoogleSearchService
    {
        Task<string> GetGoogleSearchResults(string searchText);
    }

    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GoogleSearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetGoogleSearchResults(string searchText)
        {
            var httpClient = _httpClientFactory.CreateClient();  
            httpClient.BaseAddress = new Uri("https://www.google.com.au"); 

            var result = await httpClient.GetAsync($"/search?num=100&q={searchText}");
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();

            return content;
        }
    }
}