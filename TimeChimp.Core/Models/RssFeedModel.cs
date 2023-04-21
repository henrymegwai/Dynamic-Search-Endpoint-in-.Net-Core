using System;
using System.Collections.Generic;
using System.Text;
using TimeChimp.Core.Managers;
using TimeChimp.Core.Utlilities;

namespace TimeChimp.Core.Models
{
    public class RssFeedModel
    {
        public PaginatedList<RssFeedItem> RssFeedItems { get; set; }
    }
}
