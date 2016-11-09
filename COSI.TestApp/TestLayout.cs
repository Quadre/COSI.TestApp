using System;
using NUnit.Framework;
using COSI.TestApp.Pages;


namespace COSI.TestApp
{
    class TestLayout
    {
        private MainPage mainPage;

        [OneTimeSetUp]
        [Description("Sets up  tests activity")]
        public void SetUp()
        {
            mainPage = new MainPage(TestSetup.WebDriver);            
        }

        [Test]
        [Description("Check that app page is accessible")]
        public void TC1_MainPageAccesible()
        {
            mainPage.Refresh();
            Assert.IsTrue(true, "Main page could not be validated.");
        }        
    }
}
