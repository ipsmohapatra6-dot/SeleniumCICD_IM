using OpenQA.Selenium;

namespace DotNetSeleniumProject.Pages
{
    public class MultipleWindowHelper
    {
        private IWebDriver driver;
        public MultipleWindowHelper(IWebDriver driver)
        {
            this.driver = driver;

        }
        public void SwitchToWindowByTitle(string title)
        {
            foreach (var handle in driver.WindowHandles)
            {
                driver.SwitchTo().Window(handle);
                if (driver.Title.Contains(title))
                {
                    return;
                }
            }
            throw new Exception($"Window with title {title} not found");
        }
        public void SwitchToMainWindow()
        {
            var handle = driver.WindowHandles[0];
            driver.SwitchTo().Window(handle);
        }
    }
}
