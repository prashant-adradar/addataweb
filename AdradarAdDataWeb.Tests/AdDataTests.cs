using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AdradarAdDataWeb.Models;
using AdradarAdDataWeb.Controllers;
using AdradarAdDataWeb.AdDataServiceReference;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AdradarAdDataWeb.Tests
{
    /// <summary>
    /// Summary description for AdDataTests
    /// </summary>
    [TestClass]
    public class AdDataTests
    {
        private static Ad[] __dummyaddata;

        public AdDataTests()
        {
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod_Verify_Loaddata()
        {
            AdDataModel addatamodel = new AdDataModel();
            Assert.AreEqual(1163, addatamodel.NumberOfRows);
        }

        [TestMethod]
        public void TestMethod_Verify_Controller_callto_AdDataModel_List1_VerifyFirstGet()
        {
            //Setup
            Task<Ad[]> dummytask = new Task<Ad[]>(() => __dummyaddata);

            Mock<IAdDataService> mockAdDataService = new Mock<IAdDataService>(MockBehavior.Loose);
            mockAdDataService.Setup(f => f.GetAdDataByDateRangeAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(dummytask);

            Mock<IServiceClientFactory> mockServiceClientFactory = new Mock<IServiceClientFactory>(MockBehavior.Loose);
            mockServiceClientFactory.Setup(f => f.GetAdDataServiceClient()).Returns(mockAdDataService.Object);

            Mock<IAdDataModel> mockAdDataModel = new Mock<IAdDataModel>(MockBehavior.Loose);
            mockAdDataModel.Setup(f => f.GetAllData(1, "brandname")).Returns(new List<Ad>(__dummyaddata));

            //Act
            AdDataController controller = new AdDataController(mockAdDataModel.Object);
            ActionResult result = controller.Index(1, "brandname");

            //Assert
            Assert.AreEqual(true, result is ViewResult);
            ViewResult viewresult = result as ViewResult;

            Assert.AreEqual(true, viewresult.Model is List<Ad>);
            List<Ad> ads = viewresult.Model as List<Ad>;

            mockAdDataModel.Verify(c => c.GetAllData(It.Is<int>(p => p == 1), It.Is<string>(s => s == "brandname")), Times.Once);
            mockAdDataModel.Verify(c => c.GetAllData(It.Is<int>(p => p == 1), It.Is<string>(s => s == "brandid")), Times.Never);
            Assert.AreEqual(3, ads.Count);
        }

        static AdDataTests()
        {
            __dummyaddata = new Ad[]{
                new Ad { AdId = 1, Brand = new Brand{BrandId = 1, BrandName = "Brand Name 1" }, NumPages = 0.1M, Position = "Page" },
                new Ad { AdId = 2, Brand = new Brand{BrandId = 1, BrandName = "Brand Name 1" }, NumPages = 0.5M, Position = "Page" },
                new Ad { AdId = 3, Brand = new Brand{BrandId = 2, BrandName = "Brand Name 2" }, NumPages = 0.2M, Position = "Cover" }
            };
        }
    }
}
