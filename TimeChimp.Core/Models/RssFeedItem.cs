using System;
using System.Collections.Generic;
using System.Text;

namespace TimeChimp.Core.Models
{
    public class RssFeedItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public string Uri { get; set; }
        public List<string> Links { get; set; }
        public List<string> Category { get; set; }
        public string Author { get; set; }
        public string CopyRights { get; set; }
    }
}
