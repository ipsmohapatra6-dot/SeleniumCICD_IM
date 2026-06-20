using AventStack.ExtentReports;
using DotNetSeleniumProject.Driver;
using DotNetSeleniumProject.Extensions;
using DotNetSeleniumProject.Report;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotNetSeleniumProject.Test
{
    [TestFixture(DriverTypes.Chrome)]
    public class DemoTest
    {

        private IWebDriver _driver;
        private readonly string username;
        private readonly string password;
        private readonly DriverTypes driverType;
        private ExtentReportSetUp _extentReportsSetUp;
        private ExtentReports extentReports;
        private ExtentTest extentTest;

        public DemoTest(DriverTypes driverType)
        {
            this.driverType = driverType;
            _extentReportsSetUp = new ExtentReportSetUp();
        }

        // Initialize ExtentReports once for the fixture (all tests)
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            (extentReports, extentTest) = _extentReportsSetUp.SetUpExtentReport(); // returns a timestamped file by default
        }

        [SetUp]
        public void Setup()
        {
            _driver = GetWebDriver(driverType);
            extentTest.Log(Status.Info, "Browser is launched");
            _driver.Navigate().GoToUrl("https://demo.guru99.com/test/newtours/register.php");
            extentTest.Log(Status.Info, "Navigated to URI");
            _driver.Manage().Window.Maximize();
            extentTest.Log(Status.Info, "Window is maximized");

        }
        private IWebDriver GetWebDriver(DriverTypes driverType)
        {
            switch (driverType)
            {
                case DriverTypes.Chrome:
                    return new ChromeDriver();
                case DriverTypes.Firefox:
                    return new OpenQA.Selenium.Firefox.FirefoxDriver();
                case DriverTypes.Edge:
                    return new OpenQA.Selenium.Edge.EdgeDriver();
                default:
                    throw new ArgumentException($"Unsupported driver type: {driverType}");
            }
        }
        [Test]
        public void WorkingWithDropDownTest()
        {
            try
            {
                extentTest=extentReports.CreateTest("Working with drop down").Log(Status.Info, "Working with dropdown test starts");
                var CountryDropDown = _driver.FindElement(By.Name("country"));
                CountryDropDown.SelectDropDownByText("INDIA");
                extentTest.Log(Status.Pass, "Option is selected from dropdown by visible text");
                if (CountryDropDown.DropDownIsMultiple())
                {


                    CountryDropDown.SelectDropDownByMultipleText(["PAKISTAN", "CHINA"]);
                    var selectedOptions = CountryDropDown.GetDropDownSelectedOptions();
                    foreach (var selectedOption in selectedOptions)
                    {
                        Console.WriteLine(selectedOption);
                    }

                }
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Test failed with exception: {ex.Message}");
                throw;
            }
            finally
            {
                extentTest.Log(Status.Info, "Working with dropdown test ends");
            }
        }
        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                _extentReportsSetUp.TakeScreenshot(_driver, TestContext.CurrentContext.Test.Name);
            }
            else
            {
                extentTest.Log(Status.Pass, "Test passed");
            }
            _driver.Dispose();
            extentTest.Log(Status.Info, $"Browser is closed.");

        }

        // Flush and finalize report once after all tests
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extentReports.Flush();
            _extentReportsSetUp.SendExtentReportEmail();
            // Optionally send email here: _extentReportsSetUp.SendExtentReportEmail();
        }
    }
}
