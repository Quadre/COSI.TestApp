using System;
using NUnit.Framework;
using NUnitGoCore.Attributes;
using COSI.TestApp.Pages;

namespace COSI.TestApp
{
    [TestFixture]
    class TestLayout
    {
        private MainPage mainPage;

        [OneTimeSetUp]
        [Description("Setup  tests activity")]
        public void SetUp()
        {
            mainPage = new MainPage(TestSetup.WebDriver);            
        }

        [Test, NunitGoAction(testName: "L_TC1_MainPageAccesible")]
        [Description("Check that app page is loaded")]
        public void L_TC1_MainPageAccesible()
        {            
            Assert.IsTrue(mainPage.IsValidMainPage(), "Main page not recognized");
        }

        [Test, NunitGoAction(testName: "L_TC2_TreeLayoutSize")]
        [Description("Check req 2.1.3.3 35% tree size")]
        public void L_TC2_TreeLayoutSize()
        {
            //todo: consider check with different window sizes            
            // default windows size
            Assert.AreEqual(35, mainPage.GetTreeViewportInPercent(), 0.5);
        }
    }
}
