using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using TimeChimp.Core.Interfaces.IManagers;
using TimeChimp.Core.Utlilities;

namespace TimeChimp.Backend.Assessment.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [AllowAnonymous]
    public class RssFeedController : Controller
    {
        private readonly IRssFeedManager _rssFeedManager;

        public RssFeedController(IRssFeedManager rssFeedManager)
        {
            _rssFeedManager = rssFeedManager; 
        }
        public class Example
        {
            [Required]
            public Guid Id { get; set; }

            public string Message { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchquery"></param>
        /// <param name="sortBy"> title, title_desc, date, date_desc</param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("search/{searchquery}/{sortBy}/{pageNumber}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Search(string searchquery, string sortBy = "title", int pageNumber = 1, int pageSize = 20)
        {
            var rssFeedPaginated = _rssFeedManager.SearchRssFeeds(searchquery, currentFilter: "All", sortBy, pageNumber, pageSize);
            return Ok(rssFeedPaginated);
        }

    }
}