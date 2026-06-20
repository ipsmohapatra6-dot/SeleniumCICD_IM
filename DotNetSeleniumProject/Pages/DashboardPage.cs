using OpenQA.Selenium;

namespace DotNetSeleniumProject.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver driver;

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        IWebElement DashboardHeader
        {
            get
            {
                return driver.FindElement(By.ClassName("dash-header"));
            }
        }
        IWebElement BtnRefresh => driver.FindElement(By.XPath("//a[@class='refresh-btn']"));

        public (bool, bool) DahsboardHeaderAndRefreshBtnAreDisplayed()
        {
            return (DashboardHeader.Displayed, BtnRefresh.Displayed);
        }
    }
}
