using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace COSI.TestApp
{
    [SetUpFixture]
    public class TestSetup
    {
        public static IWebDriver WebDriver { get; private set; }        

        [OneTimeSetUp]
        [Description("Configure tests activity")]
        public void SetUp()
        {
            

            // think about muti-browser support
            WebDriver = new ChromeDriver();

            // configure default timeouts
            //WebDriver.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(Config.DefaultTimeoutSec));
        }

        [OneTimeTearDown]
        [Description("Tear down test activity")]
        public void TearDown()
        {
            WebDriver.Quit();
        }
    }
}
