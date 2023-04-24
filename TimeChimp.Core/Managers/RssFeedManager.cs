using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using TimeChimp.Core.Interfaces.IManagers;
using TimeChimp.Core.Interfaces.IServices;
using TimeChimp.Core.Configs;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using TimeChimp.Core.Utlilities;
using TimeChimp.Core.Models;

namespace TimeChimp.Core.Managers
{
    public class RssFeedManager : IRssFeedManager
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IRssFeedService _rssFeedService;
        private readonly RssFeedConfig _optionsRssFeedConfig;

        public RssFeedManager(IRssFeedService rssFeedService, IMemoryCache memoryCache, IOptions<RssFeedConfig> optionsRssFeedConfig)
        {
            _rssFeedService = rssFeedService;
            _memoryCache = memoryCache;
            _optionsRssFeedConfig = optionsRssFeedConfig.Value;
        }
        public List<RssFeedItem> LoadRssFeed()
        {
            var listofItems = new List<RssFeedItem> { };
            if (!_memoryCache.TryGetValue(_optionsRssFeedConfig.RssFeedMemoryCacheKey, out listofItems))
            {
                var rssFeedResults = _rssFeedService.LoadRssFeed(_optionsRssFeedConfig.RssFeedUrl);
                listofItems = rssFeedResults.Select(x => new RssFeedItem
                {
                    Id = x.Id,
                    Title = x.Title?.Text,
                    Description = x.Summary?.Text,
                    PublishDate = x.PublishDate.UtcDateTime,
                    Links = x.Links.Select(x => x.Uri.AbsoluteUri).ToList(),
                    Uri = x.Links.FirstOrDefault().Uri.AbsoluteUri,
                    Category = x.Categories.Select(x => x.Name).ToList(),
                    Author = SyndicationExtensions.GetExtensions(x, "creator"),
                    CopyRights = SyndicationExtensions.GetExtensions(x, "rights")
                }).ToList();

                if (rssFeedResults.Count() > 0)
                {
                    _memoryCache.Set(_optionsRssFeedConfig.RssFeedMemoryCacheKey, listofItems, new MemoryCacheEntryOptions()
                         .SetSlidingExpiration(TimeSpan.FromSeconds(_optionsRssFeedConfig.RssFeedMemoryCacheExpiryInSecs))
                         );
                }

            }
            return listofItems;

        }

        public PaginatedList<RssFeedItem> SearchRssFeeds(string query, string currentFilter, string sortBy, int pageNumber = 1, int pageSize = 20)
        {
            // Load Rss Feed from Cache or the Source
            var rssFeeds = LoadRssFeed();

            if (rssFeeds.Count() == 0)
                return PaginatedList<RssFeedItem>.CreateAsync(null, pageNumber, pageSize);

            query = string.IsNullOrEmpty(query) ? currentFilter : query;

            var rssFeedsResults = query == currentFilter ? rssFeeds : rssFeeds.Where(p => p.Title.Contains(query) || p.Description.Contains(query) || p.Category.Contains(query) || p.Author.Contains(query));
            
            RssFeedSortOrder sortOrder = string.IsNullOrEmpty(sortBy) ? RssFeedSortOrder.title : sortBy.ToEnum<RssFeedSortOrder>();
            switch (sortOrder)
            {
                case RssFeedSortOrder.title:
                    rssFeedsResults = rssFeedsResults.OrderBy(x => x.Title);
                    break;
                case RssFeedSortOrder.title_desc:
                    rssFeedsResults = rssFeedsResults.OrderByDescending(x => x.Title);
                    break;
                case RssFeedSortOrder.date:
                    rssFeedsResults = rssFeedsResults.OrderBy(x => x.PublishDate);
                    break;
                case RssFeedSortOrder.date_desc:
                    rssFeedsResults = rssFeedsResults.OrderByDescending(x => x.PublishDate);
                    break;
            }
            return PaginatedList<RssFeedItem>.CreateAsync(rssFeedsResults.ToList(), pageNumber, pageSize);
        }


        public ExecutionResponse<PaginatedList<RssFeedItem>> SearchRssFeedsV2(string query, string currentFilter, string sortBy, int pageNumber = 1, int pageSize = 20)
        {
            // Load Rss Feed from Cache or the Source
            var rssFeeds = LoadRssFeed();

            if (rssFeeds.Count() == 0)
                return new ExecutionResponse<PaginatedList<RssFeedItem>>();

            query = string.IsNullOrEmpty(query) ? currentFilter : query;

            var rssFeedsResults = query == currentFilter ? rssFeeds : rssFeeds.Where(p => p.Title.Contains(query) || p.Description.Contains(query) || p.Category.Contains(query) || p.Author.Contains(query));

            RssFeedSortOrder sortOrder = string.IsNullOrEmpty(sortBy) ? RssFeedSortOrder.title : sortBy.ToEnum<RssFeedSortOrder>();
            switch (sortOrder)
            {
                case RssFeedSortOrder.title:
                    rssFeedsResults = rssFeedsResults.OrderBy(x => x.Title);
                    break;
                case RssFeedSortOrder.title_desc:
                    rssFeedsResults = rssFeedsResults.OrderByDescending(x => x.Title);
                    break;
                case RssFeedSortOrder.date:
                    rssFeedsResults = rssFeedsResults.OrderBy(x => x.PublishDate);
                    break;
                case RssFeedSortOrder.date_desc:
                    rssFeedsResults = rssFeedsResults.OrderByDescending(x => x.PublishDate);
                    break;
            }
            var pageResult = PaginatedList<RssFeedItem>.CreateAsync(rssFeedsResults.ToList(), pageNumber, pageSize);
            ExecutionResponse<PaginatedList<RssFeedItem>> response = new ExecutionResponse<PaginatedList<RssFeedItem>>();
            response.Data = pageResult;
            response.PageNumber = pageResult.PageIndex;
            response.ItemCount = pageResult.Items;
            response.Status = true;
            response.Message = pageResult.Count > 0 ? "Successful" : "No record(s) found";
            return response;
        }
    }

}
