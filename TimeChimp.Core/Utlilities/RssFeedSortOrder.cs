using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimeChimp.Core.Utlilities
{
    public enum RssFeedSortOrder
    {
        [Description("Title Ascending")]
        title = 1,
        [Description("Title Descending")]
        title_desc,
        [Description("Date Ascending")]
        date,
        [Description("Date Descending")]
        date_desc
    }
}
