using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdradarAdDataWeb.AdDataServiceReference
{
    class BrandMetadata
    {
        [Display(Name="Brand Id")]
        public int BrandId { get; set; }
        [Display(Name="Brand Name")]
        public string BrandName { get; set; }
    }
}