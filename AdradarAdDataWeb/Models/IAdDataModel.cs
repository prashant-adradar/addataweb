using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdradarAdDataWeb.Models
{
    public interface IAdDataModel
    {
        List<AdradarAdDataWeb.AdDataServiceReference.Ad> GetAllData(int pagenumber, string sortby);
        List<AdradarAdDataWeb.AdDataServiceReference.Ad> GetForPositionAtleastNumPages(int pagenumber, string sortby, decimal numpages, string position);
        List<AdDataModel.GroupByBrandVM> GetTopNAdsByBrandByCoverage(int topN);
        List<AdDataModel.GroupTopNBrandNameVM> GetTopNByTotalCoverage(int topN);
        int NumberOfRows { get; set; }
        Task<bool> LoadData();
    }
}
