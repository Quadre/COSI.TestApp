using System;
using System.Collections.Generic;
using NUnit.Framework;
using COSI.TestApp.Pages;


namespace COSI.TestApp
{
    class TestFunc
    {
        private MainPage mp;

        [OneTimeSetUp]
        [Description("Setup tests activity")]
        public void SetUp()
        {
            mp = new MainPage(TestSetup.WebDriver);
        }

        [TestCase("Z")]
        [Description("1.2.1 Grid Add.")]
        public void Fn_TC1_AddToGrid(string letter)
        {
            mp.Refresh();
            List<string> gridItems = mp.GetGridItems();

            // remove before add in exists
            if (gridItems.Contains(letter))
            {
                mp.DelGridItem(letter);
            }

            gridItems = mp.GetGridItems();
            Assert.IsFalse(gridItems.Contains(letter), "req 1.2.1 'delete' letter failed, 'Add' could not be checked");

            mp.AddGridItem(letter);
            gridItems = mp.GetGridItems();

            Assert.IsTrue(gridItems.Contains(letter), "req 1.2.1 'Add' do not add letter {0} to grid", letter);
        }

        [TestCase("Z")]
        [Description("1.2.4 Delete.")]
        public void Fn_TC2_DelGridItem(string letter)
        {
            mp.Refresh();
            List<string> before = mp.GetGridItems();

            before = mp.GetGridItems();
            Assert.IsTrue(before.Contains(letter), "req 1.2.4 letter {0} not exists in grid, could not check", letter);

            mp.DelGridItem(letter);
            before = mp.GetGridItems();

            Assert.IsFalse(before.Contains(letter), "req 1.2.4 'Delete' do not delete letter {0} from grid", letter);
        }


        [TestCase("A")]
        [Description("1.3.1 Add.")]
        public void Fn_TC3_AddToTree(string letter)
        {
            mp.Refresh();
            List<string> before = mp.GetTreeItems();

            // remove before add in exists
            if (before.Contains(letter))
            {
                mp.DelTreeItem(letter);
            }

            before = mp.GetTreeItems();
            Assert.IsFalse(before.Contains(letter), "req 1.3.1 'delete' letter failed, 'Add' could not be checked");

            mp.AddTreeItem(letter);
            before = mp.GetTreeItems();

            Assert.IsTrue(before.Contains(letter), "req 1.3.1 'Add' do not add letter {0} to tree", letter);
        }

        [TestCase("A")]
        [Description("1.3.4 Delete.")]
        public void Fn_TC4_DelTreeItem(string letter)
        {
            mp.Refresh();
            List<string> before = mp.GetTreeItems();

            before = mp.GetTreeItems();
            Assert.IsTrue(before.Contains(letter), "req 1.3.4 letter {0} not exists in Tree, could not check", letter);

            mp.DelTreeItem(letter);
            before = mp.GetTreeItems();

            Assert.IsFalse(before.Contains(letter), "req 1.3.4 'Delete' do not delete letter {0} from Tree", letter);
        }

        [TestCase("A")]
        // [TestCase("C")] //...
        [Description("1.1.1.1 Drag-and-drop. User should be able to DD one item from tree to grid.")]
        public void Fn_TC5_DnDTree2Grid(string letter)
        {
            mp.Refresh();
            List<string> listGrid = mp.GetGridItems();
            List<string> listTree = mp.GetTreeItems();

            Assert.IsFalse(listGrid.Contains(letter) == true || listTree.Contains(letter) == false, 
                    "req 1.1.1.1 Drag-and-drop. Preconditions failed", letter);            

            mp.DragNDropTree2Grid(letter);

            listGrid = mp.GetGridItems();
            listTree = mp.GetTreeItems();

            Assert.IsTrue(listGrid.Contains(letter) == true && listTree.Contains(letter) == false, 
                    "req 1.1.1.1 Drag-and-drop. Failed", letter);
        }

        [TestCase("Z")]
        [Description("1.1.1.2 Drag-and-drop. User should be able to DD one item from grid to tree.")]
        public void Fn_TC6_DnDGrid2Tree(string letter)
        {
            mp.Refresh();
            List<string> listGrid = mp.GetGridItems();
            List<string> listTree = mp.GetTreeItems();

            Assert.IsFalse(listTree.Contains(letter) == true || listGrid.Contains(letter) == false,
                    "req 1.1.1.2 Drag-and-drop. Preconditions failed", letter);

            mp.DragNDropGrid2Tree(letter);

            listGrid = mp.GetGridItems();
            listTree = mp.GetTreeItems();

            Assert.IsTrue(listTree.Contains(letter) == true && listGrid.Contains(letter) == false,
                    "req 1.1.1.2 Drag-and-drop. Failed", letter);
        }
    }
}
