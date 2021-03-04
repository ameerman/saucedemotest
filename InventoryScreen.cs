using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SauceDemo
{
    public class InventoryScreen : SauceDemoScreen
    {
        private const string inventoryItemCLASSNAME = "inventory_item";
        private const string inventory_item_nameCLASSNAME = "inventory_item_name";
        private const string inventory_item_priceCLASSNAME = "inventory_item_price";
        private List<IWebElement> inventoryContainers => driver.FindElementsById("inventory_container").ToList();
        private IWebElement checkoutItem => driver.FindElementById("shopping_cart_container");
        private IWebElement sortCombo => driver.FindElementByClassName("product_sort_container");
        private IWebElement basketCounter => driver.FindElementByClassName("fa-layers-counter");

        public InventoryScreen(ChromeDriver drvr, WebDriverWait waitcntrl) 
                               : base(drvr, waitcntrl)
        {
        }
        
        public bool IsDisplayed()
        {
            return inventoryContainers.Count > 0;
        }

        public CartScreen ClickCheckoutIcon()
        {
            checkoutItem.Click();
            return new CartScreen(driver, waitcontroller);
        }

        public InventoryScreen SelectOrder(ItemSortOrder ordercode)
        {
            SelectElement orderSelector = new SelectElement(sortCombo);
            orderSelector.SelectByValue(ordercode.ToString());

            return this;
        }
        
        public List<InventoryItem> GetListOfItemsInOrder()
        {
            var itemList = new List<InventoryItem>();
            foreach (var item in driver.FindElementsByClassName("inventory_item"))
            {
                itemList.Add(new InventoryItem(item));
            }
            return itemList;
        }

        public int GetNumberOfItemsInBasketFromIcon()
        {
            int result = 0;
            try
            {
                int.TryParse(basketCounter.Text, out result);
            }
            catch { }
            return result;
        }
    }
}
