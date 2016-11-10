using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;

namespace COSI.TestApp.Pages
{
    class MainPage
    {
        public readonly string xpTreeDiv = "//div[@id='dd-tree-1011']";
        public readonly string xpGridDiv = "//div[@id='gridview-1021']";

        public readonly string xpTreeRootItem = "//div[@id='dd-tree-1011']//tr[@data-recordid='root']";
        public readonly string xpTreeItems = "//table[@id='treeview-1017-table']//span[@class='x-tree-node-text ']";
        public readonly string xpGridItems = "//div[@id='dd-grid-1018']//table/tbody/tr/td[2]/div[@class='x-grid-cell-inner ']";
        public readonly string xpTreeItemByLetter = "//table[@id='treeview-1017-table']//span[@class='x-tree-node-text ' and text()='{0}']";
        public readonly string xpGridItemByLetter = "//div[@id='dd-grid-1018']//table/tbody/tr/td[2]/div[@class='x-grid-cell-inner ' and text()='{0}']";

        public readonly string xpPopupAddBtn = "//a[@id='menuitem-1013-itemEl']";
        public readonly string xpPopupDelBtn = "//a[@id='menuitem-1014-itemEl']";

        public readonly string xpAddBtn = "//span[@id='button-1023-btnEl']";
        public readonly string xpDelBtn = "//span[@id='button-1024-btnEl']";

        public readonly string xpDialogInputBox = "//input[@id='combo-1029-inputEl']";     
        public readonly string xpDialogOkBtn = "//span[@id='button-1031-btnEl']";


        protected IWebDriver wd;        

        public MainPage(IWebDriver webDriver)
        {
            if (webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
            this.wd = webDriver;
            //this.cfg = cfg;

            //HtmlElementLoader.PopulatePageObject(this, webDriver);
            PageFactory.InitElements(webDriver, this);

            Refresh();
        }
        /// <summary>      
        /// Loads app under test into current webDriver
        /// </summary>
        public void Refresh()
        {
            string solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory));
            wd.Navigate().GoToUrl(solution_dir + "\\..\\COSI.App\\index.html");
        }

        /// <summary>
        /// Validate page based on some criteria (To be clarified)
        /// </summary>
        /// <returns>Valid = tru</returns>
        public bool IsValidMainPage()
        {
            return wd.Title.ToLower().Contains("dd");
        }

        /// <summary>
        /// Check req 2.1.3.3 'Left part is 35% of horizontal viewportsize '
        /// </summary>
        /// <returns>Left part horizontal viewportsize in %</returns>
        public double GetTreeViewportInPercent()
        {
            int viewportWidth = wd.Manage().Window.Size.Width;
            int treeWidth = wd.FindElement(By.XPath(xpTreeDiv)).Size.Width;
            return (double)treeWidth / viewportWidth * 100.0;
        }

        /// <summary />        
        /// <returns>Returns a list of tree items in app</returns>
        public List<string> GetTreeItems()
        {
            // skip a root element 
            return wd.FindElements(By.XPath(xpTreeItems)).Skip(1).Select(q => q.GetAttribute("innerHTML")).ToList();
        }

        /// <summary />        
        /// <returns>Returns a list of grid items in app</returns>
        public List<string> GetGridItems()
        {
            // skip a headers row by xPath
            return wd.FindElements(By.XPath(xpGridItems)).Select(q => q.GetAttribute("innerHTML")).ToList();
        }

        /// <summary>
        /// Add an letter/item to a tree
        /// </summary>
        /// <param name="item">Letter item to add</param>
        public void AddTreeItem(string item)
        {
            IWebElement treeItem = wd.FindElement(By.XPath(xpTreeRootItem));            

            Actions builder = new Actions(wd);
            builder.MoveToElement(treeItem);
            builder.ContextClick(treeItem).Build().Perform();

            IWebElement btn = wd.FindElement(By.XPath(xpPopupAddBtn));

            Assert.IsTrue(btn.Displayed && btn.Enabled, "Add button not visible or disabled", item);
            btn.Click();

            DialogBoxInputAndPressOk(item);
        }

        /// <summary>
        /// Delete a pre dedicated element from Tree
        /// </summary>
        /// <param name="item">Item to delete</param>
        public void DelTreeItem(string item)
        {
            IWebElement treeItem = wd.FindElement(By.XPath(string.Format(xpTreeItemByLetter, item)));
           
            Actions builder = new Actions(wd);
            builder.MoveToElement(treeItem);
            builder.ContextClick(treeItem).Build().Perform();  

            IWebElement btn = wd.FindElement(By.XPath(xpPopupDelBtn));
            
            Assert.IsTrue(btn.Displayed && btn.Enabled, "Delete button not visible or disabled", item);

            btn.Click();            
        }

        /// <summary>
        /// Drag n drop letter item from  tree to grid
        /// </summary>
        /// <param name="item">Item / letter to drag & drop</param>
        public void DragNDropTree2Grid(string item)
        {
            IWebElement treeItem = wd.FindElement(By.XPath(string.Format(xpTreeItemByLetter, item)));
            IWebElement gridDiv = wd.FindElement(By.XPath(xpGridDiv));
         
            Actions builder = new Actions(wd);

            treeItem.Click();

            IAction dragAndDrop = builder.ClickAndHold(treeItem).MoveToElement(gridDiv).Release(gridDiv).Build();
            dragAndDrop.Perform();
        }

        public void AddGridItem(string item)
        {
            IWebElement btn = wd.FindElement(By.XPath(xpAddBtn));

            Assert.IsTrue(btn.Displayed && btn.Enabled, "Add button not visible or disabled", item);
            btn.Click();
            DialogBoxInputAndPressOk(item);
        }

        public void DelGridItem(string item)
        {
            IWebElement gridItem = wd.FindElement(By.XPath(string.Format(xpGridItemByLetter, item)));
           
            Actions builder = new Actions(wd);
            builder.MoveToElement(gridItem);
            builder.ContextClick(gridItem).Build().Perform();  

            IWebElement btn = wd.FindElement(By.XPath(xpDelBtn));
            
            Assert.IsTrue(btn.Displayed && btn.Enabled, "Delete button not visible or disabled", item);

            btn.Click();   
        }

        public void DragNDropGrid2Tree(string item)
        {
            IWebElement gridItem = wd.FindElement(By.XPath(string.Format(xpGridItemByLetter, item)));
            IWebElement treeDiv = wd.FindElement(By.XPath(xpTreeRootItem));

            Actions builder = new Actions(wd);

            gridItem.Click();

            IAction dragAndDrop = builder.ClickAndHold(gridItem).MoveToElement(treeDiv).Release(treeDiv).Build();
            dragAndDrop.Perform();
        }



        /// <summary>
        /// Type text (letter) into dialog box 
        /// </summary>
        /// <param name="item"></param>
        private void DialogBoxInputAndPressOk(string item)
        {
            IWebElement input = wd.FindElement(By.XPath(xpDialogInputBox));
            IWebElement okBtn = wd.FindElement(By.XPath(xpDialogOkBtn));
            Assert.IsTrue(input.Displayed && input.Enabled && okBtn.Displayed && okBtn.Enabled, "Dialog box either not visible or disabled", item);

            input.SendKeys(item);
            System.Threading.Thread.Sleep(1000);
            okBtn.Click();                        
        }
    }
}
