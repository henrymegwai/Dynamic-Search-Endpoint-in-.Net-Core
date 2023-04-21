using System;
using System.Collections.Generic;
using System.Text;

namespace TimeChimp.Core.Configs
{
    public class RssFeedConfig
    {
        public string RssFeedUrl { get; set; }
        public string RssFeedMemoryCacheKey { get; set; }
        public int RssFeedMemoryCacheExpiryInSecs { get; set; } = 36000;
    }
}
