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
            IEnumerable<SyndicationItem> rssFeedResults = null;
            if (!_memoryCache.TryGetValue(_optionsRssFeedConfig.RssFeedMemoryCacheKey, out rssFeedResults))
            {
                rssFeedResults = _rssFeedService.LoadRssFeed(_optionsRssFeedConfig.RssFeedUrl);
                if (rssFeedResults != null)
                {
                    _memoryCache.Set(_optionsRssFeedConfig.RssFeedMemoryCacheKey, rssFeedResults, new MemoryCacheEntryOptions()
                         .SetSlidingExpiration(TimeSpan.FromSeconds(_optionsRssFeedConfig.RssFeedMemoryCacheExpiryInSecs))
                         );
                }

            }
            var listofItems = new List<RssFeedItem> { };
            foreach (var item in rssFeedResults)
            {
                listofItems.Add(item.ConvertToNewsItem());
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
    }

}
