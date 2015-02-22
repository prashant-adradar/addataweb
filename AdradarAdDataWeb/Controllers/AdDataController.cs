﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdradarAdDataWeb.Models;
using AdradarAdDataWeb.AdDataServiceReference;

namespace AdradarAdDataWeb.Controllers
{
    public class AdDataController : Controller
    {
        private IAdDataModel __model = null;

        public AdDataController()
        {
            __model = new AdDataModel();
        }

        public AdDataController(IAdDataModel model)
        {
            __model = model;
        }

        public ActionResult Index(int pagenumber, string sortby)
        {
            try
            {
                sortby = (sortby == null) ? "" : sortby;
                List<Ad> data = __model.GetAllData(pagenumber, sortby);

                ViewBag.AdDataPagerData = new PagerData { NumberOfRows = __model.NumberOfRows, NumberOfRowsPerPage = AdDataModel.NUMBER_OF_ROWS_PER_PAGE, CurrentPageNumber = pagenumber, Action = "Index", Controller = "AdData", SortBy = sortby };

                return View(data);
            }
            catch(Exception ex)
            {
                return ErrorPage(ex);
            }
        }

        public ActionResult List2(int pagenumber, string sortby)
        {
            try
            {
                sortby = (sortby == null) ? "" : sortby;
                List<Ad> data = __model.GetForPositionAtleastNumPages(pagenumber, sortby, 0.5M, "Cover");

                ViewBag.AdDataPagerData = new PagerData { NumberOfRows = __model.NumberOfRows, NumberOfRowsPerPage = AdDataModel.NUMBER_OF_ROWS_PER_PAGE, CurrentPageNumber = pagenumber, Action = "List2", Controller = "AdData", SortBy = sortby };

                return View(data);
            }
            catch(Exception ex)
            {
                return ErrorPage(ex);
            }
        }

        public ActionResult List3()
        {
            try
            {
                List<AdDataModel.GroupByBrandVM> data = __model.GetTopNAdsByBrandByCoverage(5);

                return View(data);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }

        public ActionResult List4()
        {
            try
            {
                List<AdDataModel.GroupTopNBrandNameVM> data = __model.GetTopNByTotalCoverage(5);

                return View(data);
            }
            catch (Exception ex)
            {
                return ErrorPage(ex);
            }
        }

        public ActionResult ErrorPage(Exception ex)
        {
            return View("ErrorPage", ex);
        }
    }
}