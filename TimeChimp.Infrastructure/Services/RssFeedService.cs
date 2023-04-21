using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TimeChimp.Core.Interfaces.IServices;

namespace TimeChimp.Infrastructure.Services
{
    public class RssFeedService : IRssFeedService
    {
        public List<SyndicationItem> LoadRssFeed(string sourceUrl)
        {
            using var reader = XmlReader.Create(sourceUrl);
            var feed = SyndicationFeed.Load(reader);
            return feed.Items.ToList();
        }
    }
}
