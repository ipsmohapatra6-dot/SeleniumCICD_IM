using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotNetSeleniumProject.Driver;
using DotNetSeleniumProject.Extensions;
using DotNetSeleniumProject.Pages;
using DotNetSeleniumProject.Report;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DotNetSeleniumProject.Test
{
    [TestFixture("admin", "password", DriverTypes.Chrome)]

    public class NUnitTestDemo
    {
        private IWebDriver _driver;
        private readonly string username;
        private readonly string password;
        private readonly DriverTypes driverType;
        private ExtentReportSetUp _extentReportsSetUp;
        private ExtentReports extentReports;
        private ExtentTest extentTest;
        private ExtentTest _testNode;

        public NUnitTestDemo(string username, string password, DriverTypes driverType)
        {
            this.username = username;
            this.password = password;
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
            //_extentReportsSetUp.SetUpExtentReport();
            _driver = GetWebDriver(driverType);
            _testNode = extentTest.CreateNode("Setup and Tear Down").Pass("Browser Launched");
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");            
            _driver.Manage().Window.Maximize();
         

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
        //private void SetUpExtentReport()
        //{
        //    _extentReports = new ExtentReports();
        //    var spark=new ExtentSparkReporter("ExtentReport.html");
        //    _extentReports.AttachReporter(spark);
        //    _extentReports.AddSystemInfo("OS", "Windows 11");
        //    _extentReports.AddSystemInfo("Browser",driverType.ToString());
        //    _extentTest = _extentReports.CreateTest("Login Test with POM").Log(Status.Info,"Extent report initialized");

        //}

        [Test]
        [Category("smoke")]
        public void EAWebSiteLoginTestWithPOM()
        {
            try
            {
                extentTest = extentReports.CreateTest("Login Test For Employees Site").Log(Status.Info, "Log in Test with POM starts");
                //Click on log in link
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.ClickLoginLink();
                extentTest.Log(Status.Pass, "Log in link clicked.");
                loginPage.Login(username, password);
                extentTest.Log(Status.Pass, "Username and password entered and log in happened upon clicking log in Btn.");
                var (isHelloAdminDisplayed, isDashboardDisplayed) = loginPage.HelloAdminAndDashboardIsDisplayed();
                Assert.IsTrue(isHelloAdminDisplayed && isDashboardDisplayed);
                isHelloAdminDisplayed.Should().BeTrue();
                isDashboardDisplayed.Should().BeTrue();
                extentTest.Log(Status.Pass, "Employee home page is displayed with Hello Admin and Dashboard tabs.");
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Test failed with exception: {ex.Message}");
                throw;

            }
            finally
            {
                extentTest.Log(Status.Info, "Log in Test with POM ends");
            }
        }

        [Test]
        [Category("smoke")]
        public void EAWebSiteDashboardTestWithPOM()
        {
            try
            {
                extentTest = extentReports.CreateTest("Dashboard Test For Employees Site").Log(Status.Info, "Dashboard Test with POM starts");
               
                LoginPage loginPage = new LoginPage(_driver);
                loginPage.ClickDashboardLink();
                extentTest.Log(Status.Pass, "Dashboard link is clicked.");

                DashboardPage dashboardPage = new DashboardPage(_driver);
                var (isDashboardHeaderDisplayed, isRefreshBtnDisplayed) = dashboardPage.DahsboardHeaderAndRefreshBtnAreDisplayed();

                isDashboardHeaderDisplayed.Should().BeTrue();
                isRefreshBtnDisplayed.Should().BeTrue();
                extentTest.Log(Status.Pass, "Dashboard page is displayed with Dashboard Header and Refresh button.");
            }
            catch (Exception ex)
            {
                extentTest.Log(Status.Fail, $"Test failed with exception: {ex.Message}");
                throw;

            }
            finally
            {
                extentTest.Log(Status.Info, "Dashboard test with POM ends");
            }
        }


        [Test]
        [TestCase("Chrome", "45")]
        public void TestBrowserVersion(string broserName, string version)
        {
            Console.WriteLine($"Browser Name: {broserName} and version is {version}");
        }

        [Test]
        //Fluent wait is a form of explicit wait allows to set the maximum wait time, polling interval and also ignore exception types
        //Useful for handeling dynamic elements that appear unpredictabily on screen
        public void TestFluentWait()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(_driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(30);
            fluentWait.PollingInterval = TimeSpan.FromSeconds(5);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            IWebElement element = fluentWait.Until(x => x.FindElement(By.Id("UserName")));
            element.EnterText("admin");
        }

        [Test]
        //Waits for a particular element to load for the specified timeout
        //Halts the execution for a particular condition to be met : like visibility,clickability,presence
        public void TestExplicitWait()
        {
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName")));
            //element.Click();
            IWebElement element = wait.Until(driver =>
            {
                var el = driver.FindElement(By.Id("UserName"));
                return el.Displayed ? el : null;
            });
            element.Click();
        }

        [Test]
        //Sets wait condition for all elements on the page
        public void TestImplicitWait()
        {
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            var element = _driver.FindElement(By.Id("UserName"));

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
