using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace SeoMonitor.SearchRankings
{
    public interface IGoogleResultMatcher
    {
        List<SearchRanking> FindMatchingSearchResults(string searchEngineResults, string websiteToMatch);
    } 

    public class GoogleResultMatcher : IGoogleResultMatcher
    {
        public List<SearchRanking> FindMatchingSearchResults(string searchEngineResults, string websiteToMatch)
        {
            var fullLinkPattern = "url\\?q=.+?(?=<\\/a>)";
            var urlPattern = "url\\?q=[^>]+\"";
            var titleDivAndUrlDivPattern = "<div.+?(?=</div>)";
            
            var fullLinkRegexMatch = new Regex(fullLinkPattern);
            var urlRegexMatch = new Regex(urlPattern);
            var titleDivAndUrlDivMatch = new Regex(titleDivAndUrlDivPattern);

            MatchCollection searchResultLinks = fullLinkRegexMatch.Matches(searchEngineResults);
            var searchResultLinksList = 
                searchResultLinks
                    .Select(srl => srl.Value)
                    .ToList();

            searchResultLinksList.RemoveAt(searchResultLinksList.Count - 1);

            var searchRankings = searchResultLinksList
                .Select(srl => {
                    int rank = searchResultLinksList.IndexOf(srl) + 1;

                    var dirtyUrl = urlRegexMatch.Matches(srl).Single();
                    var parsedUrl = dirtyUrl.Value.Remove(dirtyUrl.Value.IndexOf("&amp;sa")).Replace("url?q=", "");

                    var dirtyTitleDiv = titleDivAndUrlDivMatch
                        .Matches(srl)
                        .Select(m => m.Value)
                        .FirstOrDefault();
                    
                    var title = dirtyTitleDiv is null ? "<TITLE MISSING>" : dirtyTitleDiv.Remove(0, dirtyTitleDiv.IndexOf("\">"))?.Replace("\">", "");
                    
                    var isMatch = parsedUrl is null ? false : parsedUrl.Contains(websiteToMatch);

                    return new SearchRanking(rank, parsedUrl, title, isMatch);
                }).ToList();

            return searchRankings;

        }
    }
}