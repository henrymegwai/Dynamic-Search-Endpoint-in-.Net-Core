using System;
using System.Collections.Generic;
using System.Text;

namespace TimeChimp.Core.Models
{
    public class SearchModel
    {
        List<string> SortOrders { get; set; } = new List<string>
        {
           "Title",
           "Title_Descending",
           "Date",
           "Date_Descending"
        };
    }
}
