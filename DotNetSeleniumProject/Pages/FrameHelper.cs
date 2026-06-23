using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotNetSeleniumProject.Pages
{
    public class FrameHelper
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        public FrameHelper(IWebDriver driver, int WaitInSeconds=10)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(WaitInSeconds));
        }
        public void SwitchToFrame(By locator)
        {
            IWebElement frame = wait.Until(d => d.FindElement(By.TagName("iframe")));
            driver.SwitchTo().Frame(frame);
        }
        public void SwitchToFrameByIndex(int index)
        {
            wait.Until(d => d.SwitchTo().Frame(index));
           
        }
        public void SwitchToFrameByName(string name)
        {
            wait.Until(d => d.SwitchTo().Frame(name));

        }
        public void SwitchToFrameById(string id)
        {
            wait.Until(d => d.SwitchTo().Frame(id));

        }
        public void SwitchToParentFrame()
        {
            wait.Until(d => d.SwitchTo().ParentFrame());

        }
        public void SwitchToDefaultContent()
        {
            wait.Until(d => d.SwitchTo().DefaultContent());

        }
    }
}
