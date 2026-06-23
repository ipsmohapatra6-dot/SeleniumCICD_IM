using DotNetSeleniumProject.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotNetSeleniumProject.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        IWebElement LoginLink
        {
            get
            {
                return driver.FindElement(By.LinkText("Login"));
            }
        }
        IWebElement TxtUsername => driver.FindElement(By.Id("UserName"));
        IWebElement TxtPassword => driver.FindElement(By.Name("Password"));
        IWebElement BtnLogin => driver.FindElement(By.CssSelector(".btn"));

        IWebElement LnkHelloAdmin => driver.FindElement(By.LinkText("Hello admin!"));

        IWebElement LnkDashboard => driver.FindElement(By.PartialLinkText("Dashboard"));

        public void ClickLoginLink()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            LoginLink.ClickElement();
            //SeleniumCustomMethods.ClickElement(LoginLink);
        }
        public void Login(string username, string password)
        {
            TxtUsername.EnterText(username);
            TxtPassword.EnterText(password);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(50));
            //var element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("UserName")));
            //element.Click();
            IWebElement element = wait.Until(driver =>
            {
                var el = BtnLogin;
                return el.Displayed ? el : null;
            });
            SeleniumCustomMethods.ScrollToMiddle(driver, element);
            element.ClickElement();
            //BtnLogin.ClickElement();
        }
        public (bool, bool) HelloAdminAndDashboardIsDisplayed()
        {
            return (LnkHelloAdmin.Displayed, LnkDashboard.Displayed);
        }
        public void ClickDashboardLink()
        {
            LnkDashboard.ClickElement();
            //SeleniumCustomMethods.ClickElement(LoginLink);
        }


    }
}
