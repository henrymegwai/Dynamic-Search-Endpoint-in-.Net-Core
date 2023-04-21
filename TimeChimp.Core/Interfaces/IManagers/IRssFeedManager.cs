using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using TimeChimp.Core.Managers;
using TimeChimp.Core.Models;
using TimeChimp.Core.Utlilities;

namespace TimeChimp.Core.Interfaces.IManagers
{
    public interface IRssFeedManager
    {
        List<RssFeedItem> LoadRssFeed();
        PaginatedList<RssFeedItem> SearchRssFeeds(string query, string currentFilter, string sortby, int pageNumber = 1, int pageSize = 20);
    }
}
