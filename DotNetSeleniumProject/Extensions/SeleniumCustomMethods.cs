using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace DotNetSeleniumProject.Extensions
{
    public static class SeleniumCustomMethods
    {
        public static void ClickElement(this IWebElement locator)
        {
            locator.Click();
        }
        public static void EnterText(this IWebElement locator,string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
        public static void SelectDropDownByText(this IWebElement locator , string text)
        {
            SelectElement select=new SelectElement(locator);
            select.SelectByText(text);
        }
        public static void SelectDropDownByValue(this IWebElement locator, string value)
        {
            SelectElement select = new SelectElement(locator);
            select.SelectByValue(value);
        }
        public static void SelectDropDownByIndex(this IWebElement locator, int index)
        {
            SelectElement select = new SelectElement(locator);
            select.SelectByIndex(index);
        }
        public static void SelectDropDownByMultipleText(this IWebElement locator, string[] texts)
        {
            SelectElement select = new SelectElement(locator);
            for (int i = 0; i < texts.Length; i++)
            {
                select.SelectByText(texts[i]);
            }
        }
        public static void Submit(this IWebElement locator)
        {
            locator.Submit();
        }
        public static void ScrollToMiddle(this IWebDriver driver,IWebElement webElement)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", webElement);
           
        }
        public static bool DropDownIsMultiple(this IWebElement locator)
        {
            SelectElement select = new SelectElement(locator);
            return select.IsMultiple;
        }
        public static List<string> GetDropDownSelectedOptions(this IWebElement locator)
        {
            List<string> options = new List<string>();
            SelectElement select = new SelectElement(locator);
                IList<IWebElement> dropDownOptions= select.AllSelectedOptions;
            foreach(var dropDownOption in dropDownOptions)
            {
                options.Add(dropDownOption.Text);
            }
            return options;
        }
        public static void Keys_Return(this IWebElement locator)
        {
            locator.SendKeys(Keys.Return);
        }
    }
}
