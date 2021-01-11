using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SeoMonitor.SearchRankings
{
    public class SearchRanking
    {
        public int Rank { get; private set; }
        public bool IsMatch { get; private set; }
        public Link Link { get; private set; }
        public SearchRanking(int rank, string linkUri, string linkTitle, bool isMatch)
        {
            Rank = rank;
            IsMatch = isMatch;
            Link = new Link(linkTitle, linkUri);
        }
    }

    public class Link
    {
        public string Title { get; private set; }
        public string Uri { get; private set; }
        public Link(string title, string uri)
        {
            Title = title;
            Uri = uri;
        }
    }


    public class SearchRankingsRequest : IRequest<List<SearchRanking>>
    {
        public string SearchText { get; private set; }
        public Uri Website { get; private set; }
        public SearchRankingsRequest(string searchText, Uri website)
        {
            SearchText = searchText;
            Website = website;
        }
        
    }

    public class SearchRankingsRequestHandler : IRequestHandler<SearchRankingsRequest, List<SearchRanking>>
    {
        private readonly IGoogleResultMatcher _googleResultMatcher;
        private readonly IGoogleSearchService _googleSearchService;

        public SearchRankingsRequestHandler(IGoogleResultMatcher googleResultMatcher, IGoogleSearchService googleSearchService)
        {
            _googleResultMatcher = googleResultMatcher;
            _googleSearchService = googleSearchService;
        }

        public async Task<List<SearchRanking>> Handle(SearchRankingsRequest request, CancellationToken cancellationToken)
        {
            var googleResult = await _googleSearchService.GetGoogleSearchResults(request.SearchText);
            return _googleResultMatcher.FindMatchingSearchResults(googleResult, request.Website.ToString());
        }
    }
}