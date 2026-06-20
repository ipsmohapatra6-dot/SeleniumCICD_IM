using DotNetSeleniumProject.Model;
using DotNetSeleniumProject.Pages;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotNetSeleniumProject.Test
{
    public class DataDrivenTesting
    {

        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            _driver.Manage().Window.Maximize();
        }

        [Test]
        [Category("ddt")]
        [TestCaseSource(nameof(GetLogInDataFromJson))]
        public void EAWebSiteTestWithPOM(LoginModel loginModel)
        {
            //Click on log in link
            LoginPage loginPage = new LoginPage(_driver);

            loginPage.ClickLoginLink();
            loginPage.Login(loginModel.Username, loginModel.Password);
            var (isHelloAdminDisplayed, isDashboardDisplayed) = loginPage.HelloAdminAndDashboardIsDisplayed();
            //Assert.IsTrue(isHelloAdminDisplayed && isDashboardDisplayed);
            isHelloAdminDisplayed.Should().BeTrue();
            isDashboardDisplayed.Should().BeTrue();
        }
        //public static IEnumerable<LoginModel> GetLoginData()
        //{
        //    yield return new LoginModel 
        //    { 
        //        Username = "admin",
        //        Password = "password"
        //    };
        //    yield return new LoginModel
        //    {
        //        Username = "admin1",
        //        Password = "password"
        //    };
        //    yield return new LoginModel
        //    {
        //        Username = "admin2",
        //        Password = "password"
        //    };

        //} 
        public static IEnumerable<LoginModel> GetLogInDataFromJson()
        {
            string jsonpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LoginData.json");
            string jsonData = File.ReadAllText(jsonpath);
            IList<LoginModel> loginData = Newtonsoft.Json.JsonConvert.DeserializeObject<IList<LoginModel>>(jsonData);
            foreach (var login in loginData)
            {
                yield return login;
            }
        }


        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }
    }
}

