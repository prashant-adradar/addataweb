using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdradarTrialApp.AdDataServiceReference;

namespace AdradarTrialApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ListAds();

            //Top5ByDistinctCoverageCount();
            //Top5ByCoverageTotal();

            //Top5ByAdsByCoverageByBrand();

            //ListCoverPositionAtleast50Percent();

            //ListAllData();

            //Console.ReadKey();
        }

        static void ListAllData()
        {
            Ad[] result = GetAds();

            var listAll = from ad in result
                          orderby ad.Brand.BrandName
                          select ad;

            foreach (var rowlistAll in listAll)
            {
                Console.WriteLine("{0},\"{1}\",{2},\"{3}\"", rowlistAll.AdId, rowlistAll.Brand.BrandName, rowlistAll.NumPages, rowlistAll.Position);
            }
        }

        static void ListCoverPositionAtleast50Percent()
        {
            Ad[] result = GetAds();

            var coverPositionAtleast50Percent = from ad in result
                                                where ad.NumPages >= 0.5M && ad.Position == "Cover"
                                                orderby ad.Brand.BrandName
                                                select ad;

            foreach (var rowcoverPositionAtleast50Percent in coverPositionAtleast50Percent)
            {
                Console.WriteLine("{0},\"{1}\",{2},\"{3}\"", rowcoverPositionAtleast50Percent.AdId, rowcoverPositionAtleast50Percent.Brand.BrandName, rowcoverPositionAtleast50Percent.NumPages, rowcoverPositionAtleast50Percent.Position);
            }
        }

        static void Top5ByAdsByCoverageByBrand()
        {
            Ad[] result = GetAds();

            var top5byBrandNumPages = from ad in result
                                      group ad by ad.Brand.BrandName
                                          into g
                                          orderby g.Key
                                          select new
                                          {
                                              BrandName = g.Key,
                                              Subgroup = (from ad in g
                                                          orderby ad.NumPages descending
                                                          select ad).Take(5)
                                          };

            foreach (var rowbyBrand in top5byBrandNumPages)
            {
                //Console.WriteLine("\"{0}\" ==>", rowbyBrand.BrandName);
                foreach (var rowad in rowbyBrand.Subgroup)
                {
                    //Console.WriteLine("       {0},\"{1}\",{2}", rowad.AdId, rowad.Brand.BrandName, rowad.NumPages);
                    Console.WriteLine("{0},\"{1}\",{2}", rowad.AdId, rowad.Brand.BrandName, rowad.NumPages);
                }
            }
        }

        static void Top5ByCoverageTotal()
        {
            Ad[] result = GetAds();

            var groupbyBrandTotalNumPages = result
                        .OrderBy(o => o.Brand.BrandName)
                        .GroupBy(g => new { BrandName = g.Brand.BrandName })
                        .Select(s => new { s.Key.BrandName, TotalNumPages = s.Sum(r => r.NumPages) })
                        .OrderByDescending(o => o.TotalNumPages).ThenBy(o => o.BrandName);

            var rowNumberWithPartitionBy = groupbyBrandTotalNumPages.Take(5)
                                            .ToList();

            foreach (var rowgroupbyBrandTotalNumPages in rowNumberWithPartitionBy)
            {
                Console.WriteLine("\"{0}\",\"{1}\"", rowgroupbyBrandTotalNumPages.BrandName, rowgroupbyBrandTotalNumPages.TotalNumPages);
            }
        }

        static void Top5ByDistinctCoverageCount()
        {
            Ad[] result = GetAds();

            var groupbyBrandNumPages = result
                        .OrderBy(o => o.Brand.BrandName).ThenBy(o => o.NumPages)
                        .GroupBy(g => new { BrandName = g.Brand.BrandName, NumPages = g.NumPages })
                        .Select(s => new { s.Key.BrandName, s.Key.NumPages, Count = s.Count() })
                        .OrderBy(o => o.BrandName).ThenBy(o => o.NumPages).ThenByDescending(o => o.Count);

            var rowNumberWithPartitionBy = groupbyBrandNumPages
                        .GroupBy(g => new { BrandId = g.BrandName })
                        .Select(s => new { s, BrandNameWiseCount = s.Count() })
                        .SelectMany(sm => sm.s.Select(s => s)
                                              .Zip(Enumerable.Range(1, sm.BrandNameWiseCount),
                                                    (group, index) => new { group.BrandName, group.NumPages, group.Count, Index = index })
                        //.Where(w => w.Index <= 5)
                        )
                        .ToList();

            foreach (var rowgroupbyBrandNumPages in rowNumberWithPartitionBy)
            {
                Console.WriteLine("{0},\"{1}\",\"{2}\",{3}", rowgroupbyBrandNumPages.Index, rowgroupbyBrandNumPages.BrandName, rowgroupbyBrandNumPages.NumPages, rowgroupbyBrandNumPages.Count);
            }
        }

        static void ListAds()
        {
            Ad[] result = null;

            Task<Ad[]> ads1 = GetDataAsync();
            ads1.Wait();
            result = ads1.Result;

            //result = GetData2();

            PrintAds(result);
        }

        static Ad[] GetAds()
        {
            Ad[] result = null;

            Task<Ad[]> ads1 = GetDataAsync();
            ads1.Wait();
            result = ads1.Result;

            return result;
        }

        static async Task<Ad[]> GetDataAsync()
        {
            AdDataServiceClient client = new AdDataServiceClient();
            DateTime startDate = DateTime.Parse("1/1/2011");
            DateTime endDate = DateTime.Parse("4/1/2011");
            Ad[] ads = await client.GetAdDataByDateRangeAsync(startDate, endDate);
            return ads;
        }

        static Ad[] GetData()
        {
            AdDataServiceClient client = new AdDataServiceClient();
            DateTime startDate = DateTime.Parse("1/1/2011");
            DateTime endDate = DateTime.Parse("4/1/2011");
            Ad[] ads = client.GetAdDataByDateRange(startDate, endDate);
            return ads;
        }

        static void PrintAds(Ad[] ads)
        {
            foreach (Ad ad in ads)
            {
                Console.WriteLine(ad.AdId + "," + ad.Brand.BrandId + ",\"" + ad.Brand.BrandName + "\"," + ad.NumPages + "," + ad.Position);
            }
            Console.WriteLine();
            Console.WriteLine("Count: " + ads.Length);
        }
    }
}
