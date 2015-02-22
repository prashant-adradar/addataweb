using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AdradarAdDataWeb.AdDataServiceReference
{
    class AdMetadata
    {
        [Display(Name = "Ad Id")]
        public int AdId { get; set; }
        [Display(Name = "Num Pages")]
        public decimal NumPages { get; set; }
        [Display(Name = "Position")]
        public string Position { get; set; }
    }
}
