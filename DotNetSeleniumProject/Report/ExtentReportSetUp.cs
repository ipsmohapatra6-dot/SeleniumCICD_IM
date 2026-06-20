using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using DotNetSeleniumProject.Driver;
using FluentAssertions;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface;
using System.Net;
using System.Net.Mail;

namespace DotNetSeleniumProject.Report
{
    [TestFixture(DriverTypes.Chrome)]
    public class ExtentReportSetUp
    {
        private ExtentReports _extentReports;
        private ExtentTest _extentTest;
        private readonly DriverTypes driverType;

        public (ExtentReports extentReports, ExtentTest extentTest) SetUpExtentReport()
        {


            if (_extentReports == null)
            {
                _extentReports = new ExtentReports();
                // Use a timestamped filename so previous runs are preserved.
                //var filename = filePath ?? $"ExtentReport-{DateTime.Now:yyyyMMdd-HHmmss}.html";
                var spark = new ExtentSparkReporter("ExtentReport.html");
                _extentReports.AttachReporter(spark);
                _extentReports.AddSystemInfo("OS", "Windows 11");
                _extentReports.AddSystemInfo("Browser", driverType.ToString());
            }

            // Create a test placeholder if needed. Actual per-test ExtentTest should be created by the test class.
            _extentTest = _extentReports.CreateTest("Test Suite Initialization");//.Log(Status.Info, "Extent report initialized");
            return (_extentReports, _extentTest);

        }
        public void SendExtentReportEmail()
        {
            try
            {
                string reportPath = @"C:\Users\naren\source\repos\DotNetSeleniumProject\DotNetSeleniumProject\bin\Debug\net8.0\ExtentReport.html"; // Path to your report

                using (var mail = new MailMessage())
                using (var attachment = new Attachment(reportPath))
                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    mail.From = new MailAddress("FromMailAddress");
                    mail.To.Add("");
                    mail.Subject = "Selenium Test Execution Report";
                    mail.Body = "Please find attached.";
                    mail.Attachments.Add(attachment);

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("mailaddress", "<APP_PASSWORD>");                    
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                    smtp.Send(mail);
                }

                Console.WriteLine("Extent Report emailed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
        public void TakeScreenshot(IWebDriver driver, string screenshotName)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            string path = @"C:\Reports\" + screenshotName + ".png";
            screenshot.SaveAsFile(path);
            _extentTest.AddScreenCaptureFromPath(path);
        }

    }
}
