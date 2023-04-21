using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml.Linq;
using TimeChimp.Core.Models;

namespace TimeChimp.Core.Utlilities
{
    public static class SyndicationExtensions
    {
        public static RssFeedItem ConvertToNewsItem(this SyndicationItem item)
        {
            return new RssFeedItem
            {
                Id = item.Id,
                Title = item.Title?.Text,
                Description = item.Summary?.Text,
                PublishDate = item.PublishDate.UtcDateTime,
                Links = item.Links.Select(x=>x.Uri.AbsoluteUri).ToList(),
                Uri = item.Links.FirstOrDefault().Uri.AbsoluteUri,
                Category = item.Categories.Select(x => x.Name).ToList(),
                Author = GetExtensions(item, "creator"),
                CopyRights =  GetExtensions(item, "rights")
            };
        }

        public static string GetExtensions(SyndicationItem item, string filter)
        {
            string content = "";
            SyndicationElementExtension ext = item.ElementExtensions.FirstOrDefault(x => x.OuterName == filter);
            content = ext == null ? "" : ext.GetObject<XElement>().Name.LocalName == filter ? ext.GetObject<XElement>().Value : "";
            return content;
        }
    }
} 
