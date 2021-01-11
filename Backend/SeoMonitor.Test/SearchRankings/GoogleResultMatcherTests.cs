using System;
using System.IO;
using System.Reflection;
using Xunit;
using SeoMonitor.SearchRankings;
using System.Threading;
using System.Collections.Generic;
using FluentAssertions;

namespace SeoMonitor.Test.SearchRankings
{
    public class GoogleResultMatcherTests
    {
        [Fact]
        public void FindMatchingSearchResults_EfilingIntegration_AccurateList()
        {
            var googleResults = System.IO.File.ReadAllText(GetGoogleSearchResultsFile("efiling-integration.html"));
            
            var sut = new GoogleResultMatcher();
            var result = sut.FindMatchingSearchResults(googleResults, "www.infotrack.com");

            var expected = new List<SearchRanking>{
                new SearchRanking(1, "https://www.g2.com/categories/e-filing", "Best E-Filing Platforms in 2021 | G2", false),
                new SearchRanking(2, "https://www.infotrack.com/efiling/", "eFiling - InfoTrack", true),
                new SearchRanking(3, "https://www.infotrack.com/clio/", "eFiling integration for Clio users - InfoTrack", true),
                new SearchRanking(4, "https://www.courts.state.co.us/efiling/", "Integrated Colorado Courts E-Filing System - Colorado Judicial ...", false),
                new SearchRanking(5, "https://support.clio.com/hc/en-us/articles/360041064413-How-to-eFile-with-the-InfoTrack-Integration", "How to eFile with the InfoTrack Integration &#8211; Clio Help Center", false),
                new SearchRanking(6, "https://www.leap.us/legal-software-integrations/e-filing-infotrack/", "InfoTrack Integration - Legal Efiling for Lawyers | LEAP US", false),
                new SearchRanking(7, "http://efile.illinoiscourts.gov/service-providers.htm", "Electronic Filing Service Providers (EFSPs) - eFileIL - Illinois Courts", false),
                new SearchRanking(8, "https://www.efiletexas.gov/Service-Providers/1eFile.htm", "Court Filing Texas | eFileTexas.gov", false),
                new SearchRanking(9, "https://uslegalpro.com/courtefilingautomation", "eFiling Automation (Batch Filing) solution for Texas, Indiana, Illinois ...", false),
                new SearchRanking(10, "https://www.in.gov/judiciary/4275.htm", "Indiana's e-filing model - IN.gov", false)
            };

            result.Should().BeEquivalentTo(expected);
        }

        private string GetGoogleSearchResultsFile(string fileName)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var projectDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(currentDirectory).FullName).FullName).FullName;
            return Path.Join(projectDirectory, "SearchRankings", "GoogleSearchResults", fileName);
        }
    }
}