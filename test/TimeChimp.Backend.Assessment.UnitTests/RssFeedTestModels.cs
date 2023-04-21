using System;
using System.Collections.Generic;
using System.Text;
using TimeChimp.Core.Models;

namespace TimeChimp.Backend.Assessment.UnitTests
{
    public static class GetRssFeedItemsList
    {
        public static List<RssFeedItem> GetList()
        {
            return new List<RssFeedItem> 
            {
                new RssFeedItem
                {
                    Id ="https://www.nu.nl/-/6260332/",
                    Title = "Nick Schilder kondigt zijn eerste solotournee aan",
                    Description = "Acteur Alec Baldwin wordt niet vervolgd voor een dodelijk schietincident op de set van &lt;em&gt;Rust&lt;/em&gt;. Cameravrouw Halyna Hutchins kwam daarbij om het leven",
                    PublishDate = DateTime.Now,
                    Uri = "https://www.nu.nl/muziek/6260333/nick-schilder-kondigt-zijn-eerste-solotournee-aan.html",
                    Links = new List<string>{"https://google.com", "https://abc.com"},
                    Category = new List<string>{ "Muziek", "Media en Cultuur"},
                    Author = "onze nieuwsredactie",
                    CopyRights = "copyright photo: ANP"
                },
                new RssFeedItem
                {
                    Id ="https://www.nu.nl/-/6260332/",
                    Title = "Acteur Alec Baldwin niet vervolgd voor dodelijk schietincident op filmset",
                    Description = "Acteur Alec Baldwin wordt niet vervolgd voor een dodelijk schietincident op de set van &lt;em&gt;Rust&lt;/em&gt;. Cameravrouw Halyna Hutchins kwam daarbij om het leven",
                    PublishDate = DateTime.Now,
                    Uri = "https://www.nu.nl/entertainment/6260332/acteur-alec-baldwin-niet-vervolgd-voor-dodelijk-schietincident-op-filmset.html",
                    Links = new List<string>{"https://google.com", "https://abc.com"},
                    Category = new List<string>{ "Algemeen", "Media en Cultuur"},
                    Author = "onze nieuwsredactie",
                    CopyRights = "copyright photo: Reuters"
                },
                new RssFeedItem
                {
                    Id ="https://www.nu.nl/-/6260331/",
                    Title = "Van Gerwen al na één wedstrijd klaar in Ahoy door verlies tegen Aspinall",
                    Description = "Michael van Gerwen is donderdag al in zijn eerste wedstrijd uitgeschakeld in de Premier League-speelronde in Rotterdam. 'Mighty Mike' verloor in Ahoy met 6-5 van Nathan Aspinall.",
                    PublishDate = DateTime.Now,
                    Uri = "https://www.nu.nl/entertainment/6260332/acteur-alec-baldwin-niet-vervolgd-voor-dodelijk-schietincident-op-filmset.html",
                    Links = new List<string>{"https://google.com", "https://abc.com"},
                    Category = new List<string>{ "Sport", "Sport", "Darts"},
                    Author = "Patrick Moeke",
                    CopyRights = "copyright photo: Reuters"
                },
            };
        }
    }
}
