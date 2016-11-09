using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
//using Yandex.HtmlElements.Loaders;
//using Tests.Configuration;

namespace COSI.TestApp.Pages
{
    class MainPage
    {

        protected IWebDriver webDriver;
        //protected GeneralConfig cfg;


        public MainPage(IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
            this.webDriver = webDriver;
            //this.cfg = cfg;

            //HtmlElementLoader.PopulatePageObject(this, webDriver);
            PageFactory.InitElements(webDriver, this);
            Refresh();
        }
        /// <summary>
        /// Loads into POM page, by executing  webDriver.Navigate().GoToUrl() or similar;
        /// Loads app under test into current webDriver
        /// </summary>
        public void Navigate()
        {
            webDriver.Navigate().GoToUrl("file:///C:/Users/ODuritsyn/Downloads/QA%20Task/Alphabet_application_implementation/index.html");
        }

        public void Refresh()
        {
            webDriver.Navigate().GoToUrl("file:///C:/Users/ODuritsyn/Downloads/QA%20Task/Alphabet_application_implementation/index.html");
        }
    }
}
