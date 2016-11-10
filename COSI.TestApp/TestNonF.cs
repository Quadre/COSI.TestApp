using System;
using System.Collections.Generic;
using NUnit.Framework;
using COSI.TestApp.Pages;

namespace COSI.TestApp
{
    class TestNonF
    {
        private MainPage mp;

        [OneTimeSetUp]
        [Description("Setup tests activity")]
        public void SetUp()
        {
            mp = new MainPage(TestSetup.WebDriver);
        }

        [Test]
        [Description("req 2.2.2 After start tree node should contain 10 children elements: letters from A to J inclusive ")]
        public void NF_TC1_TreeGot10Items()
        {
            mp.Refresh();
            List<string> expectd = new List<string>() { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            List<string> actual = mp.GetTreeItems();
            Assert.IsTrue(expectd.Count == actual.Count, "Failed req 2.2.2, not all elements are in list");

            for (int i = 0; i < expectd.Count; i++)
            {
                Assert.IsTrue(expectd[i] == actual[i], "Failed req 2.2.2, expected '{0}' actual '{1}'", expectd[i],actual[i]);
            }
        }

        [Test]
        [Description("2.3.2 Grid should contain 16 rows: letters from K to Z inclusive")]
        public void NF_TC2_GridGot16Items()
        {
            mp.Refresh();
            List<string> expectd = new List<string>() { "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            List<string> actual = mp.GetGridItems();
            Assert.IsTrue(expectd.Count == actual.Count, "Failed req 2.3.2, not all elements are in list");

            for (int i = 0; i < expectd.Count; i++)
            {
                Assert.IsTrue(expectd[i] == actual[i], "Failed req 2.3.2, expected '{0}' actual '{1}'", expectd[i], actual[i]);
            }
        }
    }
}
