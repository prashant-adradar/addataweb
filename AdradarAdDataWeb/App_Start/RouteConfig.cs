using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AdradarAdDataWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AdData",
                url: "AdData/{action}/{pagenumber}/{sortby}",
                defaults: new { controller = "AdData", action = "Index", pagenumber = 1, sortby = "" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AdData", action = "Index", id = UrlParameter.Optional, pagenumber = 1, sortby = "" }
            );
        }
    }
}
