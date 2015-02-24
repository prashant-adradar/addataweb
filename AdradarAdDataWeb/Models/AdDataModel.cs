using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdradarAdDataWeb.AdDataServiceReference;
using System.Threading.Tasks;
using System.Collections;

namespace AdradarAdDataWeb.Models
{
    public class AdDataModel : IAdDataModel
    {
        private DateTime __startDate;
        private DateTime __endDate;
        private IAdDataService __addataserviceclient;
        private Ad[] __AdData;

        public const int NUMBER_OF_ROWS_PER_PAGE = 15;

        public AdDataModel()
        {
            __startDate = DateTime.Parse("1/1/2011");
            __endDate = DateTime.Parse("4/1/2011");

            IServiceClientFactory serviceclientfactory = new ServiceClientFactory();
            __addataserviceclient = serviceclientfactory.GetAdDataServiceClient();
        }

        public AdDataModel(IServiceClientFactory serviceclientfactory, DateTime startDate, DateTime endDate)
        {
            __addataserviceclient = serviceclientfactory.GetAdDataServiceClient();
            __startDate = startDate;
            __endDate = endDate;
        }

        public async Task<bool> LoadData()
        {
            __AdData = await __getDataAsync();

            NumberOfRows = __AdData.Length;

            return true;
        }

        public int NumberOfRows { get; set; }

        public List<Ad> GetAllData(int pagenumber, string sortby)
        {
            var listData = from ad in __AdData
                          select ad;

            switch (sortby)
            {
                case "adid":
                    listData = listData.OrderBy(o => o.NumPages);
                    break;
                case "brandid":
                    listData = listData.OrderBy(o => o.Brand.BrandId);
                    break;
                case "brandname":
                    listData = listData.OrderBy(o => o.Brand.BrandName);
                    break;
                case "numpages":
                    listData = listData.OrderBy(o => o.NumPages);
                    break;
                case "position":
                    listData = listData.OrderBy(o => o.Position);
                    break;
                default:
                    listData = listData.OrderBy(o => o.Brand.BrandName);
                    break;
            }

            NumberOfRows = __AdData.Length;

            List<Ad> data = listData.Skip(pagenumber * NUMBER_OF_ROWS_PER_PAGE).Take(NUMBER_OF_ROWS_PER_PAGE).ToList<Ad>();

            return data;
        }

        public List<Ad> GetForPositionAtleastNumPages(int pagenumber, string sortby, decimal numpages, string position)
        {
            var listData = from l in __AdData
                              where l.NumPages >= numpages && l.Position == position
                              select l;

            NumberOfRows = listData.ToList<Ad>().Count;

            switch (sortby)
            {
                case "adid":
                    listData = listData.OrderBy(o => o.NumPages);
                    break;
                case "brandid":
                    listData = listData.OrderBy(o => o.Brand.BrandId);
                    break;
                case "brandname":
                    listData = listData.OrderBy(o => o.Brand.BrandName);
                    break;
                case "numpages":
                    listData = listData.OrderBy(o => o.AdId);
                    break;
                default:
                    listData = listData.OrderBy(o => o.Brand.BrandName);
                    break;
            }

            List<Ad> data = listData.Skip(pagenumber * NUMBER_OF_ROWS_PER_PAGE).Take(NUMBER_OF_ROWS_PER_PAGE).ToList<Ad>();

            return data;
        }

        public List<AdDataModel.GroupByBrandVM> GetTopNAdsByBrandByCoverage(int topN)
        {
            var listData = from ad in __AdData
                                      group ad by ad.Brand.BrandName
                                          into g
                                          orderby g.Key
                                          select new AdDataModel.GroupByBrandVM
                                          {
                                              BrandName = g.Key,
                                              Subgroup = (from ad in g
                                                          orderby ad.NumPages descending
                                                          select ad).Take(5)
                                          };

            var data = listData.ToList<AdDataModel.GroupByBrandVM>();

            return data;
        }

        public List<AdDataModel.GroupTopNBrandNameVM> GetTopNByTotalCoverage(int topN)
        {
            var groupbyBrandTotalNumPages = __AdData
                        .OrderBy(o => o.Brand.BrandName)
                        .GroupBy(g => new { BrandName = g.Brand.BrandName })
                        .Select(s => new AdDataModel.GroupTopNBrandNameVM { BrandName = s.Key.BrandName, TotalNumPages = s.Sum(r => r.NumPages) })
                        .OrderByDescending(o => o.TotalNumPages).ThenBy(o => o.BrandName);

            var data = groupbyBrandTotalNumPages.Take(5).ToList<AdDataModel.GroupTopNBrandNameVM>();

            return data;
        }

        private async Task<Ad[]> __getDataAsync()
        {
            Ad[] ads = await __addataserviceclient.GetAdDataByDateRangeAsync(__startDate, __endDate);
            return ads;
        }

        public class GroupByBrandVM
        {
            public string BrandName { get; set; }
            public IEnumerable<Ad> Subgroup { get; set; }
        }

        public class GroupTopNBrandNameVM
        {
            public string BrandName { get; set; }
            public decimal TotalNumPages { get; set; }
        }
    }
}