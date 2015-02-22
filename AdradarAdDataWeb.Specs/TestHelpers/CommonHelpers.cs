using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Selenium;

namespace AdradarAdDataWeb.Specs.TestHelpers
{
    class CommonHelpers
    {
        private const string USE_BROWSER = "Chrome";
        private const string APP_URL = "http://pacific/AdradarAdDataWeb/AdData/";
        private static IWebDriver _webDriver = null;

        public static IWebDriver GetBrowserDriver()
        {
            if (_webDriver != null) return _webDriver;

            switch (USE_BROWSER)
            {
                case "Chrome":
                    _webDriver = new ChromeDriver();
                    break;
                default:
                    _webDriver = new ChromeDriver();
                    break;
            }

            return _webDriver;
        }

        public static string GetAppUrl()
        {
            return APP_URL;
        }
    }
}
