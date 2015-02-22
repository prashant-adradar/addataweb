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
    public class OpenApplicationInBrowser
    {
        [Given(@"I have opened the application in browser")]
        public void GivenIHaveOpenedTheApplicationInBrowser()
        {
            IWebDriver browserDriver = CommonHelpers.GetBrowserDriver();
        }
    }
}
