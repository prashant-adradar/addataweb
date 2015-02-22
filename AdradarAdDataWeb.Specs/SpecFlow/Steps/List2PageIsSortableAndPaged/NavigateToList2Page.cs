using AdradarAdDataWeb.Specs.TestHelpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AdradarAdDataWeb.Specs.SpecFlow.Steps.List2PageIsSortableAndPaged
{
    [Binding]
    public class NavigateToList2Page
    {
        [When(@"I navigate to List2 page")]
        public void WhenINavigateToList2Page()
        {
            IWebDriver browserDriver = CommonHelpers.GetBrowserDriver();
            browserDriver.Navigate().GoToUrl(CommonHelpers.GetAppUrl());
        }
    }
}
