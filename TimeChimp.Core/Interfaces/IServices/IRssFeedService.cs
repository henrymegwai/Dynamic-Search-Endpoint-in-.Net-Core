using System;
using System.Collections.Generic;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;

namespace TimeChimp.Core.Interfaces.IServices
{
    public interface IRssFeedService
    {
        List<SyndicationItem> LoadRssFeed(string sourceUrl);
    }
}
