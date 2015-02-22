using AdradarAdDataWeb.Specs.TestHelpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace AdradarAdDataWeb.Specs.SpecFlow.Steps.List2PageIsSortableAndPaged
{
    [Binding]
    public class ShouldDisplayPageSortableAndPaged
    {
        [Then(@"the result should be that the page is sortable and paged")]
        public void ThenTheResultShouldBeThatThePageIsSortableAndPaged()
        {
            IWebDriver browserDriver = CommonHelpers.GetBrowserDriver();

            IWebElement element_grid = browserDriver.FindElement(By.Id("addatatable"))
                .FindElement(By.ClassName("table"));
            IWebElement element_header_row = element_grid.FindElement(By.XPath("tbody/tr[count(th)>0]"));

            Assert.IsNotNull(element_grid, "List2 is not in the page");
            Assert.IsNotNull(element_header_row, "List2 table is not in the page");

            IReadOnlyCollection<IWebElement> element_header_rows = element_grid.FindElements(By.XPath("tbody/tr"));
            Assert.AreEqual(16, element_header_rows.Count);

            string[] header_column_text = new string[] { "Ad Id", "Brand Id", "Brand Name", "Num Pages", "Position" };
            foreach (string column_text in header_column_text)
            {
                IWebElement element_header_column = element_header_row.FindElement(By.LinkText(column_text));
                Assert.IsNotNull(element_header_column, "Link to sort the list by '" + column_text + "' is not found");
            }

            IWebElement element_pagectrls = browserDriver.FindElement(By.Id("pagerctrls"));
            Assert.IsNotNull(element_pagectrls, "Page controls are not in the page");

            IReadOnlyCollection<IWebElement> element_pagectrls_buttons = element_pagectrls.FindElements(By.ClassName("btn-primary"));
            Assert.AreEqual(element_pagectrls_buttons.Count, 4, "Page controls section is the page, but page buttons are not found");

            browserDriver.Close();
            browserDriver.Dispose();
            browserDriver = null;
        }
    }
}
