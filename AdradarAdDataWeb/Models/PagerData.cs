using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdradarAdDataWeb.Models
{
    public class PagerData
    {
        public int NumberOfRows { get; set; }
        public int NumberOfRowsPerPage { get; set; }
        public int CurrentPageNumber { get; set; }

        public string Action { get; set; }
        public string Controller { get; set; }
        public string SortBy { get; set; }
    }
}