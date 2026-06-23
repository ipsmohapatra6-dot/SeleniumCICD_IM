using DotNetSeleniumProject.Driver;
using DotNetSeleniumProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace DotNetSeleniumProject.Test
{
    [TestFixture("admin", "password")]
    public class SeleniumGridTest
    {

        private IWebDriver _driver;
        private readonly string username;
        private readonly string password;
        //private readonly DriverTypes driverType;

        public SeleniumGridTest(string username, string password)
        {
            this.username = username;
            this.password = password;

        }

        [SetUp]
        public void Setup()
        {
            _driver = new RemoteWebDriver(new Uri("http://localhost:4444/"), new FirefoxOptions());
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }


        [Test]
        public void EAWebSiteTestWithPOM()
        {//
            //Click on log in link
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLoginLink();
            loginPage.Login(username, password);
        }
        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                //_driver.Quit();
                _driver.Dispose();
            }
        }
    }
}
