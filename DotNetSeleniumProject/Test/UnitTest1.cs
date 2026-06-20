using DotNetSeleniumProject.Extensions;
using DotNetSeleniumProject.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotNetSeleniumProject.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Manage().Window.Maximize();
            IWebElement webelement = driver.FindElement(By.XPath("//textarea[contains(@title,'Search')]"));
            webelement.ClickElement();
            webelement.EnterText("selenium");
            webelement.Keys_Return();
            Assert.Pass();
        }
        [Test]
        public void EAWebSiteTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            driver.Manage().Window.Maximize();
            //Click on log in link
            var LoginLink = driver.FindElement(By.LinkText("Login"));
            LoginLink.ClickElement();
            //Identify user name txt box and enter username 
            var UsernameTxtBox = driver.FindElement(By.Id("UserName"));
            UsernameTxtBox.EnterText("admin");
            //Identify password txt box and enter password 
            var PasswordTxtBox = driver.FindElement(By.Name("Password"));
            PasswordTxtBox.EnterText("password");
            //Identify sign in button and submit the form
            //driver.FindElement(By.CssSelector(".btn")).Click();
            IWebElement button = driver.FindElement(By.CssSelector(".btn.btn-signin"));
            driver.ScrollToMiddle(button);
            button.Submit();

        }
        [Test]
        public void WorkingWithDropDownTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.guru99.com/test/newtours/register.php");
            driver.Manage().Window.Maximize();
            var CountryDropDown = driver.FindElement(By.Name("country"));
            CountryDropDown.SelectDropDownByText("INDIA");
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
        [Test]
        public void WorkingWithCheckBoxAndRadioButtonTest()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://demo.guru99.com/test/radio.html");
            driver.Manage().Window.Maximize();
            //Clicking on radio button

            var RadioBtn = driver.FindElement(By.XPath("//input[contains(@value,'Option 2')]"));

            RadioBtn.ClickElement();
            //Selecting checkboxes
            var CheckBox = driver.FindElement(By.XPath("//*[@id=\"vfb-6-1\"]"));
            CheckBox.ClickElement();

        }


        [Test]
        public void EAWebSiteTestWithPOM()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://eaapp.somee.com/");
            driver.Manage().Window.Maximize();
            //Click on log in link
            LoginPage loginPage = new LoginPage(driver);

            loginPage.ClickLoginLink();
            loginPage.Login("admin", "password");


        }
    }
}