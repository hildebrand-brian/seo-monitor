using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SeoMonitor.SearchRankings;
using System.Threading.Tasks;
using System.Linq;

namespace SeoMonitor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchRankingsController: ControllerBase
    {
        private readonly IMediator _mediator;

        public SearchRankingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetSearchRankings(string searchText, Uri website)
        {
            if(String.IsNullOrEmpty(searchText) || String.IsNullOrEmpty(website?.ToString())) 
                return BadRequest("Both SearchText and Website are required parameters");

            var result = await _mediator.Send(
                new SearchRankingsRequest(searchText, website));

            return Ok(result);
        }
    }
}